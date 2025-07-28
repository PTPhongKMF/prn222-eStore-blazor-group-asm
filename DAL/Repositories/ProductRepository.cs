using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories {
    public enum ProductDataSortOptions {
        None,
        PriceAscending,
        PriceDescending,
        IdAscending,
        IdDescending
    }

    public class ProductRepository {
        private readonly EStoreDatabaseContext dbContext;

        public ProductRepository(EStoreDatabaseContext dbContext) {
            this.dbContext = dbContext;
        }

        // Get all products with category information
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await dbContext.Product
                .Include(p => p.Category)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
        }

        // Get product by ID with category
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await dbContext.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        // Get products by category
        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await dbContext.Product
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
        }
            
        // Get active products only
        public async Task<List<Product>> GetActiveProductsAsync()
        {
            return await dbContext.Product
                .Include(p => p.Category)
                .Where(p => p.ActiveStatus)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
        }

        // Create new product
        public async Task<Product> CreateProductAsync(Product product)
        {
            dbContext.Product.Add(product);
            await dbContext.SaveChangesAsync();

            // Reload with category information
            await dbContext.Entry(product)
                .Reference(p => p.Category)
                .LoadAsync();

            return product;
            }

        // Update existing product
        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var existingProduct = await dbContext.Product
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (existingProduct == null)
                return null;

            // Update properties
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.ProductName = product.ProductName;
            existingProduct.Weight = product.Weight;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.UnitsInStock = product.UnitsInStock;
            existingProduct.ActiveStatus = product.ActiveStatus;
            existingProduct.ImageUrl = product.ImageUrl;

            await dbContext.SaveChangesAsync();

            // Reload with category information
            await dbContext.Entry(existingProduct)
                .Reference(p => p.Category)
                .LoadAsync();

            return existingProduct;
            }

        // Delete product (soft delete by setting ActiveStatus to false)
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await dbContext.Product
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                return false;

            product.ActiveStatus = false;
            await dbContext.SaveChangesAsync();
            return true;
            }

        // Hard delete product (permanent removal)
        public async Task<bool> HardDeleteProductAsync(int productId)
        {
            var product = await dbContext.Product
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                return false;

            dbContext.Product.Remove(product);
            await dbContext.SaveChangesAsync();
            return true;
            }

        // Check if product name exists (for validation)
        public async Task<bool> ProductNameExistsAsync(string productName, int? excludeProductId = null)
        {
            var query = dbContext.Product.Where(p => p.ProductName.ToLower() == productName.ToLower());

            if (excludeProductId.HasValue)
                query = query.Where(p => p.ProductId != excludeProductId.Value);

            return await query.AnyAsync();
        }

        // Get products with low stock
        public async Task<List<Product>> GetLowStockProductsAsync(int threshold = 10) {
            return await dbContext.Product
                .Include(p => p.Category)
                .Where(p => p.UnitsInStock <= threshold && p.ActiveStatus)
                .OrderBy(p => p.UnitsInStock)
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllActiveProduct(
            string? searchTerm = null,
            ProductDataSortOptions sortOption = ProductDataSortOptions.None,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            List<int>? categoryIds = null) {

            var query = dbContext.Product
                .Include(p => p.Category)
                .Where(p => p.ActiveStatus);

            if (!string.IsNullOrWhiteSpace(searchTerm)) {
                searchTerm = searchTerm.Trim().ToLower();
                query = query.Where(p =>
                    p.ProductName.ToLower().Contains(searchTerm) ||
                    p.Weight.ToLower().Contains(searchTerm) ||
                    p.Category.CategoryName.ToLower().Contains(searchTerm));
            }

            if (minPrice.HasValue) {
                query = query.Where(p => p.UnitPrice >= minPrice.Value);
            }
            if (maxPrice.HasValue) {
                query = query.Where(p => p.UnitPrice <= maxPrice.Value);
            }

            // Apply category filter
            if (categoryIds != null && categoryIds.Any()) {
                query = query.Where(p => categoryIds.Contains(p.CategoryId));
            }

            query = sortOption switch {
                ProductDataSortOptions.PriceAscending => query.OrderBy(p => p.UnitPrice),
                ProductDataSortOptions.PriceDescending => query.OrderByDescending(p => p.UnitPrice),
                ProductDataSortOptions.IdAscending => query.OrderBy(p => p.ProductId),
                ProductDataSortOptions.IdDescending => query.OrderByDescending(p => p.ProductId),
                _ => query
            };

            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetRandomProductsFromCategory(int categoryId, int count, int excludeProductId) {
            return await dbContext.Product
                .Where(p => p.CategoryId == categoryId
                        && p.ProductId != excludeProductId
                        && p.ActiveStatus)
                .Include(p => p.Category)
                .OrderBy(r => EF.Functions.Random())
                .Take(count)
                .ToListAsync();
        }
    }
}

