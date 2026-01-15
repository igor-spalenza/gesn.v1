using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Models.ViewModels.Offer.Category;

namespace gesn.webApp.Interfaces.Services.Offer
{
    public interface ICategoryServices
    {
        Task<Guid> AddAsync(CategoryInsertViewModel vm);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<CategorySummaryViewModel>> GetAllAsync();
        Task<CategoryDetailsViewModel> GetAsync(Guid id);
        Task<IEnumerable<CategorySummaryViewModel>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null);
        Task<bool> UpdateAsync(CategoryUpdateViewModel vm);
    }
}
