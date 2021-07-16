namespace BackendTemplate.Domain.Core.Interfaces
{
    public interface IRepository<T> :
        IRepositorySelect<T>,
        IRepositorySelectPaged<T>,
        IRepositorySelectById<T>,
        IRepositorySelectByHash<T>,
        IRepositoryInsert<T>,
        IRepositoryUpdate<T>,
        IRepositoryDisable<T>,
        IRepositoryDelete<T>,
        IRepositoryDeleteRange<T>
    where T : class
    {
        
    }
}
