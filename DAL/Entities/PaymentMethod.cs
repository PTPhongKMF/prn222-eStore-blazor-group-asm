using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class PaymentMethod
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentMethodId { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
} 