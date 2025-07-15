using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class OrderStatusDTO
{
    public int OrderStatusId { get; set; }

    [Required]
    [MaxLength(50)]
    public string StatusName { get; set; } = string.Empty;
} 