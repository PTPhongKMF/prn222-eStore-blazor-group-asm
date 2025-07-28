using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class CategoryCreateDTO
{
    [Required(ErrorMessage = "T�n danh m?c l� b?t bu?c")]
    [MaxLength(40, ErrorMessage = "T�n danh m?c kh�ng ???c v??t qu� 40 k� t?")]
    [MinLength(2, ErrorMessage = "T�n danh m?c ph?i c� �t nh?t 2 k� t?")]
    public string CategoryName { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "M� t? kh�ng ???c v??t qu� 500 k� t?")]
    public string? Description { get; set; }

    [Url(ErrorMessage = "URL h�nh ?nh kh�ng h?p l?")]
    [MaxLength(500, ErrorMessage = "URL h�nh ?nh kh�ng ???c v??t qu� 500 k� t?")]
    public string? ImageUrl { get; set; }
}