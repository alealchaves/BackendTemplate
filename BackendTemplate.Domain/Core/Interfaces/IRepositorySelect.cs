using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IRepositorySelect<T> where T : class
    {
        Task<List<T>> Select();
        Task<List<T>> Select(Expression<Func<T, bool>> predicate);
        Task<List<TDTO>> Select<TDTO>();
        Task<List<TDTO>> Select<TDTO>(Expression<Func<T, bool>> predicate);

        Task<T> SelectFirst();
        Task<T> SelectFirst(Expression<Func<T, bool>> predicate);
        Task<TDTO> SelectFirst<TDTO>();
        Task<TDTO> SelectFirst<TDTO>(Expression<Func<T, bool>> predicate);
    }
}