using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Interfaces.Repositories.Offer;
using gesn.webApp.Interfaces.Services.Offer;
using gesn.webApp.Models.ViewModels.Offer;
using Mapster;
using MapsterMapper;

namespace gesn.webApp.Infrastructure.Services.Offer
{
    public class OfferServices : IOfferService
    {
        private readonly IOfferRepository _repo;
        private readonly IMapper _mapper;

        public OfferServices(IOfferRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(OfferBaseInsertViewModel model) =>
            await _repo.AddAsync(model.Adapt<Models.Entities.Offer.Offer>());

        public async Task<bool> DeleteAsync(Guid id) =>
            await this._repo.DeleteAsync(id);

        public async Task<IEnumerable<OfferSummaryViewModel>> GetAllAsync() =>
            (await this._repo.GetAllAsync()).Adapt<IEnumerable<OfferSummaryViewModel>>();

        public async Task<OfferDetailsViewModel> GetAsync(Guid id) => 
            (await this._repo.GetAsync(id)).Adapt<OfferDetailsViewModel>();

        public async Task<OfferBaseUpdateViewModel> GetForUpdateAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null) =>
             (await this._repo.ReadAsync(template, whereAdicional, parametros)).FirstOrDefault().Adapt<OfferUpdateViewModel>();

        public async Task<IEnumerable<OfferSummaryViewModel>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null) =>
            (await this._repo.ReadAsync(template, whereAdicional, parametros)).Adapt<IEnumerable<OfferSummaryViewModel>>();

        public async Task<OfferDetailsViewModel> GetAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null) =>
           (await this._repo.ReadAsync(template, whereAdicional, parametros)).FirstOrDefault().Adapt<OfferDetailsViewModel>();

        public async Task<bool> UpdateAsync(OfferUpdateViewModel model) =>
            await this._repo.UpdateAsync(model.Adapt<Models.Entities.Offer.Offer>());
    }
}
