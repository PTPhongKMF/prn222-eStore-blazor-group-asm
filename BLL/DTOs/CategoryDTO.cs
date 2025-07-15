using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class CategoryDTO
{
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(40)]
    public string CategoryName { get; set; } = string.Empty;
} 