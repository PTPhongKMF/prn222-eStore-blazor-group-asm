using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<List<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO?> GetCategoryByIdAsync(int categoryId);
        Task<(bool Success, string Message, CategoryDTO? Category)> CreateCategoryAsync(CategoryDTO categoryDto);
        Task<(bool Success, string Message, CategoryDTO? Category)> UpdateCategoryAsync(CategoryDTO categoryDto);
        Task<(bool Success, string Message)> DeleteCategoryAsync(int categoryId);
    }
}
