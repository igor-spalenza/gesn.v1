using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Models.Entities.Global;
using gesn.webApp.Models.ViewModels.Offer.Category;

namespace gesn.webApp.Interfaces.Services.Offer
{
    public interface ICategoryService
    {
        Task<Guid> AddAsync(CategoryInsertVM vm);
        Task<bool> DeleteAsync(Guid id);
        Task<IList<CategorySummaryViewModel>> GetAllAsync();
        Task<CategoryDetailsVM> GetAsync(Guid id);
        Task<IEnumerable<CategoryDetailsVM>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null);
        Task<bool> UpdateAsync(CategoryUpdateVM vm);
    }
}
