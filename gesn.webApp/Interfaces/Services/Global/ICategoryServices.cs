using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Models.ViewModels.Global;

namespace gesn.webApp.Interfaces.Services.Global
{
    public interface ICategoryServices
    {
        Task<Guid> AddAsync(CategoryInsertViewModel vm);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<CategorySummaryViewModel>> GetAllAsync();
        Task<CategoryDetailsViewModel> GetAsync(Guid id);
        Task<CategoryUpdateViewModel> GetForUpdateAsync(Guid id);
        Task<IEnumerable<CategorySummaryViewModel>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null);
        Task<bool> UpdateAsync(CategoryUpdateViewModel vm);
    }
}
