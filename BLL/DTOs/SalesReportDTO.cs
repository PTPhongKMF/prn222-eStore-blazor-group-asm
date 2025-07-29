using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class SalesReportDTO
    {
        public DateTime OrderDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalFreight { get; set; }
        public decimal GrandTotal { get; set; }
    }

    public class SalesReportSummaryDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalFreight { get; set; }
        public decimal GrandTotal { get; set; }
        public List<SalesReportDTO> DailySales { get; set; } = new();
    }
}