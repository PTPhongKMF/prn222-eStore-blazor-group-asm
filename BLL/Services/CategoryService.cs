using AutoMapper;
using BLL.DTOs;
using BLL.Interface;
using DAL.Repositories;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(CategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllCategories();
            return mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync() 
        {
            var categories = await categoryRepository.GetAllCategoriesAsync();
            return mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int categoryId)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(categoryId);
            return category == null ? null : mapper.Map<CategoryDTO>(category);
        }

        public async Task<(bool Success, string Message, CategoryDTO? Category)> CreateCategoryAsync(CategoryCreateDTO categoryCreateDto)
        {
            try
            {
                // Check if category name already exists
                var nameExists = await categoryRepository.CategoryNameExistsAsync(categoryCreateDto.CategoryName);
                if (nameExists)
                {
                    return (false, "Tên danh mục đã tồn tại", null);
                }

                var category = mapper.Map<Category>(categoryCreateDto);
                var createdCategory = await categoryRepository.CreateCategoryAsync(category);
                var categoryDto = mapper.Map<CategoryDTO>(createdCategory);

                return (true, "Tạo danh mục thành công", categoryDto);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi tạo danh mục: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message, CategoryDTO? Category)> UpdateCategoryAsync(CategoryUpdateDTO categoryUpdateDto)
        {
            try
            {
                // Check if category exists
                var existingCategory = await categoryRepository.GetCategoryByIdAsync(categoryUpdateDto.CategoryId);
                if (existingCategory == null)
                {
                    return (false, "Không tìm thấy danh mục", null);
                }

                // Check if category name already exists (excluding current category)
                var nameExists = await categoryRepository.CategoryNameExistsAsync(categoryUpdateDto.CategoryName, categoryUpdateDto.CategoryId);
                if (nameExists)
                {
                    return (false, "Tên danh mục đã tồn tại", null);
                }

                var category = mapper.Map<Category>(categoryUpdateDto);
                var updatedCategory = await categoryRepository.UpdateCategoryAsync(category);
                
                if (updatedCategory == null)
                {
                    return (false, "Không thể cập nhật danh mục", null);
                }

                var categoryDto = mapper.Map<CategoryDTO>(updatedCategory);
                return (true, "Cập nhật danh mục thành công", categoryDto);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi cập nhật danh mục: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message)> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                // Check if category exists
                var category = await categoryRepository.GetCategoryByIdAsync(categoryId);
                if (category == null)
                {
                    return (false, "Không tìm thấy danh mục");
                }

                // Check if category has products
                var productCount = await categoryRepository.GetProductCountByCategoryAsync(categoryId);
                if (productCount > 0)
                {
                    return (false, $"Không thể xóa danh mục vì còn {productCount} sản phẩm đang sử dụng danh mục này");
                }

                var deleted = await categoryRepository.DeleteCategoryAsync(categoryId);
                if (!deleted)
                {
                    return (false, "Không thể xóa danh mục");
                }

                return (true, "Xóa danh mục thành công");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa danh mục: {ex.Message}");
            }
        }
    }
}
