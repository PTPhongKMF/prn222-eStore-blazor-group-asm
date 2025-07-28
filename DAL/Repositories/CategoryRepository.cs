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
    public class CategoryRepository {
        private readonly EStoreDatabaseContext dbContext;

        public CategoryRepository(EStoreDatabaseContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await dbContext.Category
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<List<Category>> GetAllCategories() {
            return await dbContext.Category
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await dbContext.Category
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            dbContext.Category.Add(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await dbContext.Category.FindAsync(category.CategoryId);
            if (existingCategory == null)
                return null;

            existingCategory.CategoryName = category.CategoryName;
            existingCategory.Description = category.Description;
            existingCategory.ImageUrl = category.ImageUrl;

            await dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await dbContext.Category.FindAsync(categoryId);
            if (category == null)
                return false;

            // Check if category has products
            var hasProducts = await dbContext.Product.AnyAsync(p => p.CategoryId == categoryId);
            if (hasProducts)
                return false; // Cannot delete category with products

            dbContext.Category.Remove(category);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CategoryNameExistsAsync(string categoryName, int? excludeCategoryId = null)
        {
            var query = dbContext.Category.Where(c => c.CategoryName == categoryName);
            
            if (excludeCategoryId.HasValue)
                query = query.Where(c => c.CategoryId != excludeCategoryId.Value);

            return await query.AnyAsync();
        }

        public async Task<int> GetProductCountByCategoryAsync(int categoryId)
        {
            return await dbContext.Product.CountAsync(p => p.CategoryId == categoryId);
        }
    }
}
