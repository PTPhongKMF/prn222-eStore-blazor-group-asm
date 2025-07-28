using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.Interface
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetOrdersByMemberIdAsync(int memberId);
        Task<OrderDetailPageDTO?> GetOrderDetailAsync(int orderId);
        Task<int> CreateOrderAsync(OrderDTO orderDto, List<OrderDetailDTO> details);
    }


}
