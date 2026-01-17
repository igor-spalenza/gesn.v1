using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Models.ViewModels.Global;

namespace gesn.webApp.Interfaces.Services.Global
{
    public interface ITypeService
    {
        Task<Guid> AddAsync(TypeInsertViewModel vm);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<TypeSummaryViewModel>> GetAllAsync();
        Task<TypeDetailsViewModel> GetAsync(Guid id);
        Task<TypeUpdateViewModel> GetForUpdateAsync(Guid id);
        Task<IEnumerable<TypeSummaryViewModel>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null);
        Task<bool> UpdateAsync(TypeUpdateViewModel vm);
    }
}