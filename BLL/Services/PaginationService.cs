using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services {
    public class PagedResult<T> {
        public List<T> Items { get; set; } = new();
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }

    public class PaginationService {
        public static PagedResult<T> CreatePaged<T>(
            IEnumerable<T> source,
            int page,
            int pageSize) {
            
            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages));

            var items = source
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<T> {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }
    }
} 