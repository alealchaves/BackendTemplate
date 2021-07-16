using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IRepositoryDisable<T> where T : class
    {
        Task<T> Disable(T entity);
        Task<List<T>> Disable(List<T> entities);
    }
}
