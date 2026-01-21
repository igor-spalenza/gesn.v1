using gesn.webApp.Models.ViewModels.Global;
using Mapster;
using Type = gesn.webApp.Models.Entities.Global.Type;

namespace gesn.webApp.Infrastructure.Mappings
{
    public class TypeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TypeInsertViewModel, Type>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.StateCode)
                .Map(dest => dest.Id, src => new Guid().ToString());

            config.NewConfig<TypeUpdateViewModel, Type>()
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