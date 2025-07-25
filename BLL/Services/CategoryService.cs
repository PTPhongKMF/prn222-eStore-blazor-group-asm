using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using DAL.Repositories;

namespace BLL.Services {
    public class CategoryService {
        private readonly CategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(CategoryRepository categoryRepository, IMapper mapper) {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllCategories();
            return mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}
