using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Weight { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int UnitsInStock { get; set; }

    public bool ActiveStatus { get; set; } = true;

    public string? ImageUrl { get; set; }

    public virtual CategoryDTO Category { get; set; } = null!;
} 