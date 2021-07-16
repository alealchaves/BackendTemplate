using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BackendTemplate.Infra.Data.Core.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyAppContext _databaseContext;

        public UnitOfWork(MyAppContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<T> Execute<T>(Func<Task<T>> action) where T : ServiceResult
        {
            try
            {
                using (var transaction = await _databaseContext.Database
                    .BeginTransactionAsync(IsolationLevel.Snapshot))
                {
                    var result = await action();

                    if (result.IsSuccess)
                        transaction.Commit();
                    else
                        transaction.Rollback();

                    return result;
                }
            }
            catch (Exception)
            {
                _databaseContext.Dispose();
                throw;
            }
        }       
    }
}
