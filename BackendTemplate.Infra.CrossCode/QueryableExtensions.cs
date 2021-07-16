using BackendTemplate.Domain.Core.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTemplate.Infra.CrossCode
{
    public static class QueryableExtensions
    {
        public static async Task<PagedListResponse<T>> ToPagedListAsync<T>(this IQueryable<T> query, int page, int pageSize, bool countTotal = false)
        {
            var itemsWithNext = await query.Skip((page - 1) * pageSize).Take(pageSize + 1).ToListAsync();
            var hasNextPage = (itemsWithNext.Count == pageSize + 1);
            var items = itemsWithNext.Take(pageSize).ToList();

            var count = countTotal ? query.Count() : (int?)null;
            var totalPages = countTotal ? (int)Math.Ceiling(count.Value / (double)pageSize) : (int?)null;

            var result = new PagedListResponse<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                HasNextPage = hasNextPage,
                TotalCount = count,
                TotalPages = totalPages,
                Items = items
            };

            return result;
        }
    }
}
