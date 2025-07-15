using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public enum PaymentStatus
{
    [Display(Name = "Pending")]
    Pending,
    [Display(Name = "Paid")]
    Paid
} 