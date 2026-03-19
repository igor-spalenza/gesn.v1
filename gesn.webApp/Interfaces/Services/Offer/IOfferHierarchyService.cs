using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Models.ViewModels.Offer;

namespace gesn.webApp.Interfaces.Services.Offer
{
    public interface IOfferHierarchyService
    {
        Task<Guid> AddAsync(OfferHierarchyInsertViewModel vm);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<OfferHierarchySummaryViewModel>> GetAllAsync();
        Task<OfferHierarchyDetailsViewModel> GetAsync(Guid id);
        Task<OfferHierarchyUpdateViewModel> GetForUpdateAsync(Guid id);
        Task<IEnumerable<OfferHierarchySummaryViewModel>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null);
        Task<bool> UpdateAsync(OfferHierarchyUpdateViewModel vm);
    }
}
