using System.ComponentModel.DataAnnotations;

namespace eStore.Components.Models.Auth;

public class RegisterModel
{
    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [MaxLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
    [MaxLength(30, ErrorMessage = "Mật khẩu không được vượt quá 30 ký tự")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
    [Compare(nameof(Password), ErrorMessage = "Mật khẩu xác nhận không khớp")]
    public string ConfirmPassword { get; set; } = string.Empty;
} 