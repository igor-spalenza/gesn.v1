using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Infrastructure.Repositories.Templates.Global;
using gesn.webApp.Interfaces.Repositories.Offer;
using gesn.webApp.Interfaces.Services.Offer;
using gesn.webApp.Models.ViewModels.Global;
using Mapster;
using MapsterMapper;

namespace gesn.webApp.Infrastructure.Services.Offer.Category
{
    public class CategoryServices : ICategoryServices
    {
        public readonly ICategoryRepository _repo;
        public readonly IMapper _mapper;

        public CategoryServices(ICategoryRepository repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repository;
        }

        public async Task<Guid> AddAsync(CategoryInsertViewModel model) =>
            await _repo.AddAsync(model.Adapt<gesn.webApp.Models.Entities.Global.Category>());

        public async Task<bool> DeleteAsync(Guid id) =>
                 await this._repo.DeleteAsync(id);

        public async Task<IEnumerable<CategorySummaryViewModel>> GetAllAsync() =>
            (await this._repo.ReadAsync(CategoryTemplate.CategorySummaryTemplate)).Adapt<IEnumerable<CategorySummaryViewModel>>();

        public async Task<CategoryDetailsViewModel> GetAsync(Guid id) =>
            (await _repo.GetAsync(id)).Adapt<CategoryDetailsViewModel>();

        public async Task<CategoryUpdateViewModel> GetForUpdateAsync(Guid id) =>
            (await _repo.GetAsync(id)).Adapt<CategoryUpdateViewModel>();

        public Task<IEnumerable<CategorySummaryViewModel>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(CategoryUpdateViewModel model) =>
            await _repo.UpdateAsync(model.Adapt<gesn.webApp.Models.Entities.Global.Category>());
    }
}
