using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IRepositoryDeleteRange<T> where T : class
    {
        Task DeleteRange(object[] ids);
    }
}
