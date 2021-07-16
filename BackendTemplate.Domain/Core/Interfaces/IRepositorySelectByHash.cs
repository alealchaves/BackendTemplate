using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IRepositorySelectByHash<T> where T : class
    {
        Task<T> SelectByHash(Guid hash);
        Task<TDTO> SelectByHash<TDTO>(Guid hash);

        Task<int> SelectIdByHash(Guid hash);
        Task<List<int>> SelectIdByHash(List<Guid> hashs);
    }
}
