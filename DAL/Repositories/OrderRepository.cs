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
    }

}
