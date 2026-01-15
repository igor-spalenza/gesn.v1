using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Interfaces.Repositories.Offer;
using gesn.webApp.Interfaces.Services.Offer;
using gesn.webApp.Models.ViewModels.Offer.Category;
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

        public async Task<Guid> AddAsync(CategoryInsertViewModel vm)
        {
            gesn.webApp.Models.Entities.Global.Category entity = _mapper.Map<gesn.webApp.Models.Entities.Global.Category>(vm);
            await _repo.AddAsync(entity);
            return Guid.Parse(entity.Id);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategorySummaryViewModel>> GetAllAsync()
        {
            var categories = await this._repo.GetAllAsync();
            return categories.Adapt<IList<CategorySummaryViewModel>>();
        }

        public Task<CategoryDetailsViewModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategorySummaryViewModel>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CategoryUpdateViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
