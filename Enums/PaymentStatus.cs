using System.ComponentModel.DataAnnotations;

namespace Enums;

public enum PaymentStatus
{
    [Display(Name = "Pending")]
    Pending,
    [Display(Name = "Paid")]
    Paid
} 