using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class CartDTO
{
    public int MemberId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public ProductDTO? Product { get; set; } 
} 