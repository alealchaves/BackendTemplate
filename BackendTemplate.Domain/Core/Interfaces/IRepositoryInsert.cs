using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IRepositoryInsert<T> where T : class
    {
        Task<T> Insert(T entity);
        Task<List<T>> Insert(List<T> entities);
    }
}
