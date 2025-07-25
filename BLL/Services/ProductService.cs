using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services {
    public enum ProductSortBy {
        None,
        PriceAscending,
        PriceDescending,
        IdAscending,
        IdDescending
    }

    public class ProductService {
        private readonly ProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(ProductRepository productRepository, IMapper mapper) {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        private static ProductDataSortOptions MapSortOption(ProductSortBy option) {
            return option switch {
                ProductSortBy.PriceAscending => ProductDataSortOptions.PriceAscending,
                ProductSortBy.PriceDescending => ProductDataSortOptions.PriceDescending,
                ProductSortBy.IdAscending => ProductDataSortOptions.IdAscending,
                ProductSortBy.IdDescending => ProductDataSortOptions.IdDescending,
                _ => ProductDataSortOptions.None
            };
        }

        public async Task<PagedResult<ProductDTO>> GetActiveProducts(
            int page = 1,
            int pageSize = 12,
            ProductSortBy sortOption = ProductSortBy.None,
            string? searchTerm = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            List<int>? categoryIds = null) {
            
            var repoSortOption = MapSortOption(sortOption);
            var products = await productRepository.GetAllActiveProduct(
                searchTerm, 
                repoSortOption,
                minPrice,
                maxPrice,
                categoryIds);
            
            var productDtos = mapper.Map<List<ProductDTO>>(products);

            return PaginationService.CreatePaged(productDtos, page, pageSize);
        }

        public async Task<List<ProductDTO>> GetRandomProductsFromCategory(int categoryId, int count, int excludeProductId)
        {
            var products = await productRepository.GetRandomProductsFromCategory(categoryId, count, excludeProductId);
            return mapper.Map<List<ProductDTO>>(products);
        }
    }
}
