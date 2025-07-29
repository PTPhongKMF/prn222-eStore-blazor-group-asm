using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interface
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersByMemberIdAsync(int memberId);
        Task<Order?> GetOrderWithDetailsAsync(int orderId);
        Task<Order> CreateOrderAsync(Order order);
        Task<List<Order>> GetAllOrdersAsync();
        Task<bool> UpdateOrderStatusAsync(int orderId, int statusId);
        Task<List<Order>> GetOrdersWithDetailsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }

}
