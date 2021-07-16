using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IRepositoryUpdate<T> where T : class
    {
        Task<T> Update(T entity);
        Task<List<T>> Update(List<T> entities);
    }
}
