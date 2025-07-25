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

        public async Task<List<Product>> GetRandomProductsFromCategory(int categoryId, int count, int excludeProductId)
        {
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
