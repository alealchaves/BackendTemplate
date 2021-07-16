using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.DTO
{
    public class PagedListResponse<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }

        public int? TotalPages { get; set; }
        public int? TotalCount { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
