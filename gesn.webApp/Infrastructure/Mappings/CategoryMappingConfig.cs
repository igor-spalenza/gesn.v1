using gesn.webApp.Models.Entities.Global;
using gesn.webApp.Models.ViewModels.Global;
using Mapster;

namespace gesn.webApp.Infrastructure.Mappings
{
    public class CategoryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CategoryInsertViewModel, Category>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.StateCode)
                .Map(dest => dest.Id, src => new Guid().ToString());

            config.NewConfig<CategoryUpdateViewModel, Category>()
                .IgnoreNullValues(false)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.CreatedBy)
                .Ignore(dest => dest.StateCode)
                .Map(dest => dest.LastModifiedAt, src => DateTime.UtcNow)
                .Map(dest => dest.LastModifiedBy, src => "TESTE IMPLEMENTAÇÃO ALTERAR");
        }
    }
}