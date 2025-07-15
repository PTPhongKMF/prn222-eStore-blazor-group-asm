using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class OrderDetailDTO
{
    public int OrderId { get; set; }
    public int? ProductId { get; set; }
    public string? ProductName { get; set; }

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, 1)]
    public double? Discount { get; set; }
} 