using System;
using System.ComponentModel.DataAnnotations;
using DAL.Entities;

namespace BLL.DTOs;

public class OrderDTO
{
    public int OrderId { get; set; }

    public int MemberId { get; set; }
    public string MemberEmail { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Freight { get; set; }

    public OrderStatus OrderStatus { get; set; }
    public string OrderStatusDisplay { get; set; } = string.Empty;

    public PaymentStatus PaymentStatus { get; set; }
    public string PaymentStatusDisplay { get; set; } = string.Empty;

    public int? PaymentMethodId { get; set; }
    public string? PaymentMethodName { get; set; }
} 