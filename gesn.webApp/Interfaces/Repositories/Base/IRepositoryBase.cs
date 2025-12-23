using gesn.webApp.Infrastructure.Repositories.Templates.Base;
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
        Task<IEnumerable<T>> ReadAsync(QueryTemplate? template = null, string? whereAdicional = null, object? parametros = null);
    }
}