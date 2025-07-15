using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    [Required]
    [MaxLength(40)]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Weight { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [Range(0, int.MaxValue)]
    public int UnitsInStock { get; set; }
} 