using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class CategoryDTO
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
    [MaxLength(40, ErrorMessage = "Tên danh mục không được vượt quá 40 ký tự")]
    [MinLength(2, ErrorMessage = "Tên danh mục phải có ít nhất 2 ký tự")]
    public string CategoryName { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
    public string? Description { get; set; }

    public string? ImageUrl { get; set; }
}