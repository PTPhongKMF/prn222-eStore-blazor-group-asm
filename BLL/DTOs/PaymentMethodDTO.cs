using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class PaymentMethodDTO
{
    public int PaymentMethodId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
} 