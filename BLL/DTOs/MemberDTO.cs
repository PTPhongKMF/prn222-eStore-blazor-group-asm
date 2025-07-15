using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class MemberDTO
{
    public int MemberId { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(40)]
    public string? CompanyName { get; set; }

    [MaxLength(15)]
    public string? City { get; set; }

    [MaxLength(15)]
    public string? Country { get; set; }

    [Required]
    [MaxLength(30)]
    public string Password { get; set; } = string.Empty;
} 