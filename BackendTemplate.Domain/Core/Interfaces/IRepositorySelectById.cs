using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IRepositorySelectById<T> where T : class
    {
        Task<T> SelectById(object id);
        Task<TDTO> SelectById<TDTO>(object id);
    }
}
