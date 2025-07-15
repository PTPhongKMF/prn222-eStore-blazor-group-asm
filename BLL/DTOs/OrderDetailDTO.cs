using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class OrderDetailDTO
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double? Discount { get; set; }
} 