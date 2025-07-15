using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class PaymentStatusDTO
{
    public int PaymentStatusId { get; set; }

    [Required]
    [MaxLength(50)]
    public string StatusName { get; set; } = string.Empty;
} 