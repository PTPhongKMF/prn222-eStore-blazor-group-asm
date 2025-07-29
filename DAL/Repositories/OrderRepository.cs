using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EStoreDatabaseContext _context;

        public OrderRepository(EStoreDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByMemberIdAsync(int memberId)
        {
            return await _context.Order
                .Where(o => o.MemberId == memberId)
                .Include(o => o.OrderStatus)
                .Include(o => o.PaymentStatus)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.Order
                .Where(o => o.OrderId == orderId)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Order
                .Include(o => o.Member)
                .Include(o => o.OrderStatus)
                .Include(o => o.PaymentStatus)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, int statusId)
        {
            var order = await _context.Order.FindAsync(orderId);
            if (order == null) return false;

            order.OrderStatusId = statusId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetOrdersWithDetailsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Order
                .Where(o => o.OrderDate.Date >= startDate.Date && o.OrderDate.Date <= endDate.Date)
                .Include(o => o.OrderDetails)
                .Include(o => o.Member)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
