using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService
    {
        private readonly ProductRepository productRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public ProductService(ProductRepository productRepository, CategoryRepository categoryRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        // Get all products
        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var products = await productRepository.GetAllProductsAsync();
            return mapper.Map<List<ProductDTO>>(products);
        }

        // Get product by ID
        public async Task<ProductDTO?> GetProductByIdAsync(int productId)
        {
            var product = await productRepository.GetProductByIdAsync(productId);
            return product == null ? null : mapper.Map<ProductDTO>(product);
        }

        // Get products by category
        public async Task<List<ProductDTO>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await productRepository.GetProductsByCategoryAsync(categoryId);
            return mapper.Map<List<ProductDTO>>(products);
        }

        // Get active products only
        public async Task<List<ProductDTO>> GetActiveProductsAsync()
        {
            var products = await productRepository.GetActiveProductsAsync();
            return mapper.Map<List<ProductDTO>>(products);
        }

        // Create new product
        public async Task<(bool Success, string Message, ProductDTO? Product)> CreateProductAsync(ProductCreateDTO productCreateDto)
        {
            try
            {
                // Validate category exists
                var category = await categoryRepository.GetCategoryByIdAsync(productCreateDto.CategoryId);
                if (category == null)
                {
                    return (false, "Danh mục không tồn tại", null);
                }

                // Check if product name already exists
                if (await productRepository.ProductNameExistsAsync(productCreateDto.ProductName))
                {
                    return (false, "Tên sản phẩm đã tồn tại", null);
                }

                var product = mapper.Map<Product>(productCreateDto);
                var createdProduct = await productRepository.CreateProductAsync(product);
                var productDto = mapper.Map<ProductDTO>(createdProduct);

                return (true, "Tạo sản phẩm thành công", productDto);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi tạo sản phẩm: {ex.Message}", null);
            }
        }

        // Update product
        public async Task<(bool Success, string Message, ProductDTO? Product)> UpdateProductAsync(ProductUpdateDTO productUpdateDto)
        {
            try
            {
                // Check if product exists
                var existingProduct = await productRepository.GetProductByIdAsync(productUpdateDto.ProductId);
                if (existingProduct == null)
                {
                    return (false, "Sản phẩm không tồn tại", null);
                }

                // Validate category exists
                var category = await categoryRepository.GetCategoryByIdAsync(productUpdateDto.CategoryId);
                if (category == null)
                {
                    return (false, "Danh mục không tồn tại", null);
                }

                // Check if product name already exists (excluding current product)
                if (await productRepository.ProductNameExistsAsync(productUpdateDto.ProductName, productUpdateDto.ProductId))
                {
                    return (false, "Tên sản phẩm đã tồn tại", null);
                }

                var product = mapper.Map<Product>(productUpdateDto);
                var updatedProduct = await productRepository.UpdateProductAsync(product);

                if (updatedProduct == null)
                {
                    return (false, "Không thể cập nhật sản phẩm", null);
                }

                var productDto = mapper.Map<ProductDTO>(updatedProduct);
                return (true, "Cập nhật sản phẩm thành công", productDto);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi cập nhật sản phẩm: {ex.Message}", null);
            }
        }

        // Delete product (soft delete)
        public async Task<(bool Success, string Message)> DeleteProductAsync(int productId)
        {
            try
            {
                var result = await productRepository.DeleteProductAsync(productId);
                return result
                    ? (true, "Xóa sản phẩm thành công")
                    : (false, "Sản phẩm không tồn tại");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa sản phẩm: {ex.Message}");
            }
        }

        // Hard delete product
        public async Task<(bool Success, string Message)> HardDeleteProductAsync(int productId)
        {
            try
            {
                var result = await productRepository.HardDeleteProductAsync(productId);
                return result
                    ? (true, "Xóa vĩnh viễn sản phẩm thành công")
                    : (false, "Sản phẩm không tồn tại");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa vĩnh viễn sản phẩm: {ex.Message}");
            }
        }

        // Get categories for dropdown
        public async Task<List<CategoryDTO>> GetCategoriesAsync()
        {
            var categories = await categoryRepository.GetAllCategoriesAsync();
            return mapper.Map<List<CategoryDTO>>(categories);
        }

        // Get low stock products
        public async Task<List<ProductDTO>> GetLowStockProductsAsync(int threshold = 10)
        {
            var products = await productRepository.GetLowStockProductsAsync(threshold);
            return mapper.Map<List<ProductDTO>>(products);
        }
    }
}
}
