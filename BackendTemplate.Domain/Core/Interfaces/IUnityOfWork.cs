using BackendTemplate.Domain.Core.DTO;
using System;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<T> Execute<T>(Func<Task<T>> action) where T : ServiceResult;       
    }
}
