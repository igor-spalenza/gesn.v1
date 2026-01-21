using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Infrastructure.Repositories.Templates.Global;
using gesn.webApp.Interfaces.Repositories.Global;
using gesn.webApp.Interfaces.Services.Global;
using gesn.webApp.Models.ViewModels.Global;
using Mapster;
using MapsterMapper;
using Type = gesn.webApp.Models.Entities.Global.Type;

namespace gesn.webApp.Infrastructure.Services.Global
{
    public class TypeServices : ITypeService
    {
        public readonly ITypeRepository _repo;
        public readonly IMapper _mapper;

        public TypeServices(ITypeRepository repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repository;
        }

        public async Task<Guid> AddAsync(TypeInsertViewModel model) =>
            await _repo.AddAsync(model.Adapt<Type>());

        public async Task<bool> DeleteAsync(Guid id) =>
            await this._repo.DeleteAsync(id);

        public async Task<IEnumerable<TypeSummaryViewModel>> GetAllAsync() =>
            (await this._repo.ReadAsync(TypeTemplate.TypeSummaryTemplate)).Adapt<IEnumerable<TypeSummaryViewModel>>();

        public async Task<TypeDetailsViewModel> GetAsync(Guid id) =>
            (await _repo.GetAsync(id)).Adapt<TypeDetailsViewModel>();

        public async Task<TypeUpdateViewModel> GetForUpdateAsync(Guid id) =>
            (await _repo.GetAsync(id)).Adapt<TypeUpdateViewModel>();

        public Task<IEnumerable<TypeSummaryViewModel>> ReadAsync(QueryTemplate? template = null, IList<WhereTemplate>? whereAdicional = null, object? parametros = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TypeUpdateViewModel model) =>
            await _repo.UpdateAsync(model.Adapt<Type>());
    }
}
