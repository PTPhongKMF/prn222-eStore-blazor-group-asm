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

        public async Task<List<Category>> GetAllCategories()
        {
            return await dbContext.Category
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        }
    }
}
