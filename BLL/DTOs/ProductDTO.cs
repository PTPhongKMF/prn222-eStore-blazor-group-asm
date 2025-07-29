using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục hợp lệ")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên sản phẩm phải từ 2 đến 100 ký tự")]
    public string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Trọng lượng là bắt buộc")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Trọng lượng phải từ 1 đến 20 ký tự")]
    public string Weight { get; set; } = string.Empty;

    [Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
    [DataType(DataType.Currency)]
    public decimal UnitPrice { get; set; }

    [Required(ErrorMessage = "Số lượng tồn kho là bắt buộc")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được âm")]
    public int UnitsInStock { get; set; }

    public bool ActiveStatus { get; set; } = true;

    public string? ImageUrl { get; set; }

    public virtual CategoryDTO? Category { get; set; }
}