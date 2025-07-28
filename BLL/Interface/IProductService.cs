using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interface
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int productId);
        Task<List<ProductDTO>> GetProductsByCategoryAsync(int categoryId);
        Task<List<ProductDTO>> GetActiveProductsAsync();
        Task<(bool Success, string Message, ProductDTO? Product)> CreateProductAsync(ProductCreateDTO productCreateDto);
        Task<(bool Success, string Message, ProductDTO? Product)> UpdateProductAsync(ProductUpdateDTO productUpdateDto);
        Task<(bool Success, string Message)> DeleteProductAsync(int productId);
        Task<(bool Success, string Message)> HardDeleteProductAsync(int productId);
        Task<List<CategoryDTO>> GetCategoriesAsync();
        Task<List<ProductDTO>> GetLowStockProductsAsync(int threshold = 10);
    }
}
