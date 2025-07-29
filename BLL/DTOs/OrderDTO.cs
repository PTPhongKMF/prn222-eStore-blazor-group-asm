using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class OrderDTO
{
    public int OrderId { get; set; }

    public int MemberId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public decimal Freight { get; set; }

    public int OrderStatusId { get; set; }

    public int PaymentStatusId { get; set; }

    public int? PaymentMethodId { get; set; }

    // For admin purposes
    public string? MemberEmail { get; set; }
}