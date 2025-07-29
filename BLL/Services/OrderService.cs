using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Interface;
using DAL.Entities;
using DAL.Interface;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderDTO>> GetOrdersByMemberIdAsync(int memberId)
        {
            var orders = await _orderRepository.GetOrdersByMemberIdAsync(memberId);

            return orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                MemberId = o.MemberId,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate,
                ShippedDate = o.ShippedDate,
                Freight = o.Freight,
                OrderStatusId = o.OrderStatusId,
                PaymentStatusId = o.PaymentStatusId,
                PaymentMethodId = o.PaymentMethodId
            }).ToList();
        }

        public async Task<OrderDetailPageDTO?> GetOrderDetailAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderWithDetailsAsync(orderId);
            if (order == null) return null;

            return new OrderDetailPageDTO
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Freight = order.Freight,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    Discount = od.Discount
                }).ToList()
            };
        }

        public async Task<int> CreateOrderAsync(OrderDTO orderDto, List<OrderDetailDTO> details)
        {
            // Convert OrderDTO to Order entity
            var order = _mapper.Map<Order>(orderDto);
            order.OrderDate = DateTime.Now;

            // Map details and attach to Order
            order.OrderDetails = details.Select(d => new OrderDetail
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                Discount = d.Discount
            }).ToList();

            var savedOrder = await _orderRepository.CreateOrderAsync(order);
            return savedOrder.OrderId;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            return orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                MemberId = o.MemberId,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate,
                ShippedDate = o.ShippedDate,
                Freight = o.Freight,
                OrderStatusId = o.OrderStatusId,
                PaymentStatusId = o.PaymentStatusId,
                PaymentMethodId = o.PaymentMethodId,
                // Include member info for admin
                MemberEmail = o.Member?.Email ?? "Unknown"
            }).ToList();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, int statusId)
        {
            return await _orderRepository.UpdateOrderStatusAsync(orderId, statusId);
        }

        public async Task<SalesReportSummaryDTO> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetOrdersWithDetailsByDateRangeAsync(startDate, endDate);

            // Calculate daily sales
            var dailySales = orders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new SalesReportDTO
                {
                    OrderDate = g.Key,
                    TotalOrders = g.Count(),
                    TotalSales = g.Sum(o => o.OrderDetails.Sum(d => d.UnitPrice * d.Quantity * (1 - (decimal)(d.Discount ?? 0)))),
                    TotalFreight = g.Sum(o => o.Freight),
                    GrandTotal = g.Sum(o => o.OrderDetails.Sum(d => d.UnitPrice * d.Quantity * (1 - (decimal)(d.Discount ?? 0))) + o.Freight)
                })
                .OrderByDescending(d => d.GrandTotal)
                .ToList();

            // Calculate summary
            return new SalesReportSummaryDTO
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalOrders = orders.Count,
                TotalSales = dailySales.Sum(d => d.TotalSales),
                TotalFreight = dailySales.Sum(d => d.TotalFreight),
                GrandTotal = dailySales.Sum(d => d.GrandTotal),
                DailySales = dailySales
            };
        }
    }
}


