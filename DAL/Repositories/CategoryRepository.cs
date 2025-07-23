using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository
    {
        private readonly EStoreDatabaseContext dbContext;

        public CategoryRepository(EStoreDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await dbContext.Category
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await dbContext.Category
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }
    }
}