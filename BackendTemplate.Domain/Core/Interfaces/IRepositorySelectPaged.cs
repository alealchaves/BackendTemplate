using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackendTemplate.Domain.Core.DTO;

namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IRepositorySelectPaged<T> where T : class
    {
        Task<PagedListResponse<TDTO>> SelectPaged<TDTO>(int page = 1, int pageSize = 10, bool countTotal = false);
        Task<PagedListResponse<TDTO>> SelectPaged<TDTO>(Expression<Func<T, bool>> predicate, int page = 1, int pageSize = 10, bool countTotal = false);
    }
}
