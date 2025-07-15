using System.ComponentModel.DataAnnotations;

namespace Enums;

public enum OrderStatus
{
    [Display(Name = "Pending")]
    Pending,
    [Display(Name = "Shipping")]
    Shipping,
    [Display(Name = "Complete")]
    Complete
} 