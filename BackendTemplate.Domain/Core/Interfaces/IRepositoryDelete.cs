using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IRepositoryDelete<T> where T : class
    {
        Task<int> Delete(object id);
    }
}
