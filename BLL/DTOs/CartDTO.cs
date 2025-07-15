using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class CartDTO
{
    public int MemberId { get; set; }
    public string MemberEmail { get; set; } = string.Empty;

    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
} 