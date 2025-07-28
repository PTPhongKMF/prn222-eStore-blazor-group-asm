using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class OrderDetailPageDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Freight { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new();
    }

}
