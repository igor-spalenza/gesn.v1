using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Interfaces.Repositories.Offer;
using gesn.webApp.Interfaces.Services.Offer;
using gesn.webApp.Models.ViewModels.Offer.Category;
using MapsterMapper;

namespace gesn.webApp.Infrastructure.Services.Offer.Category
{
    public class CategoryServices : ICategoryService
    {
        public readonly ICategoryRepository _repo;
        public readonly IMapper _mapper;

        public CategoryServices(ICategoryRepository repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repository;
        }

        public Task<Guid> AddAsync(CategoryInsertVM vm)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<CategorySummaryViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Models.Entities.Global.Category> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDetailsVM>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CategoryUpdateVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
