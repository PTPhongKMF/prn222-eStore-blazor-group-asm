using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class OrderStatus
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderStatusId { get; set; }

    [Required]
    [MaxLength(50)]
    public string StatusName { get; set; } = string.Empty;
} 