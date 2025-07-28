using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class CategoryCreateDTO
{
    [Required(ErrorMessage = "Tên danh m?c là b?t bu?c")]
    [MaxLength(40, ErrorMessage = "Tên danh m?c không ???c v??t quá 40 ký t?")]
    [MinLength(2, ErrorMessage = "Tên danh m?c ph?i có ít nh?t 2 ký t?")]
    public string CategoryName { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Mô t? không ???c v??t quá 500 ký t?")]
    public string? Description { get; set; }

    [Url(ErrorMessage = "URL hình ?nh không h?p l?")]
    [MaxLength(500, ErrorMessage = "URL hình ?nh không ???c v??t quá 500 ký t?")]
    public string? ImageUrl { get; set; }
}