using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Interface;
using DAL.Entities;
using DAL.Interface;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
        }

    }


