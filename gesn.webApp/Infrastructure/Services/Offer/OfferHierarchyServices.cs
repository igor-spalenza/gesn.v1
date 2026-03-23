using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Infrastructure.Repositories.Templates.Offer;
using gesn.webApp.Interfaces.Repositories.Offer;
using gesn.webApp.Interfaces.Services.Offer;
using gesn.webApp.Models.ViewModels.Offer;
using Mapster;
using MapsterMapper;

namespace gesn.webApp.Infrastructure.Services.Offer
{
    public class OfferHierarchyServices : IOfferHierarchyService
    {
        public readonly IOfferHierarchyRepository _repo;
        public readonly IMapper _mapper;

        public OfferHierarchyServices(IOfferHierarchyRepository repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repository;
        }

        public async Task<Guid> AddAsync(OfferHierarchyInsertViewModel model) =>
            await _repo.AddAsync(model.Adapt<Models.Entities.Offer.OfferHierarchy>());

        public async Task<bool> DeleteAsync(Guid id) =>
            await this._repo.DeleteAsync(id);

        public async Task<IEnumerable<OfferHierarchySummaryViewModel>> GetAllAsync() =>
            (await this._repo.ReadAsync(OfferHierarchyTemplate.SummaryTemplate)).Adapt<IEnumerable<OfferHierarchySummaryViewModel>>();

        public async Task<OfferHierarchyDetailsViewModel> GetAsync(Guid id) =>
            (await _repo.GetAsync(id)).Adapt<OfferHierarchyDetailsViewModel>();

        public async Task<OfferHierarchyUpdateViewModel> GetForUpdateAsync(Guid id) =>
            (await _repo.GetAsync(id)).Adapt<OfferHierarchyUpdateViewModel>();

        public Task<IEnumerable<OfferHierarchySummaryViewModel>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(OfferHierarchyUpdateViewModel model) =>
            await _repo.UpdateAsync(model.Adapt<Models.Entities.Offer.OfferHierarchy>());
    }
}
