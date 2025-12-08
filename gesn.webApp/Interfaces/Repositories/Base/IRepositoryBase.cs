using System.Data;

namespace gesn.webApp.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetAsync(Guid id);
        Task<IList<T>> GetAllAsync();
        Task<Guid> AddAsync(T entity, IDbTransaction? transaction = null);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(T entity);
    }
}