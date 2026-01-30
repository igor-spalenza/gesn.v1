using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Models.ViewModels.Offer;

namespace gesn.webApp.Interfaces.Services.Offer
{
    public interface IOfferService
    {
        Task<Guid> AddAsync(OfferBaseInsertViewModel vm);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<OfferSummaryViewModel>> GetAllAsync();
        Task<OfferDetailsViewModel> GetAsync(Guid id);
        Task<OfferDetailsViewModel> GetAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null);
        Task<OfferBaseUpdateViewModel> GetForUpdateAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null);
        Task<IEnumerable<OfferSummaryViewModel>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null);
        Task<bool> UpdateAsync(OfferUpdateViewModel vm);
    }
}
