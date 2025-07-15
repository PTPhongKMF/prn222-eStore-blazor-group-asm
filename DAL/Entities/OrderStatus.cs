using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public enum OrderStatus
{
    [Display(Name = "Pending")]
    Pending,
    [Display(Name = "Shipping")]
    Shipping,
    [Display(Name = "Complete")]
    Complete
} 