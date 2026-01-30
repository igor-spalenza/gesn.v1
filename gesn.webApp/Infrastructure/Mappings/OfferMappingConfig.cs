using gesn.webApp.Models.Entities.Offer;
using gesn.webApp.Models.ViewModels.Offer;
using Mapster;

namespace gesn.webApp.Infrastructure.Mappings
{
    public class OfferMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OfferInsertViewModel, Offer>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.StateCode)
                .Map(dest => dest.Id, src => new Guid().ToString());

            config.NewConfig<OfferUpdateViewModel, Offer>()
                .IgnoreNullValues(false)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.CreatedBy)
                //.Ignore(dest => dest.StateCode)
                .Map(dest => dest.LastModifiedAt, src => DateTime.UtcNow)
                .Map(dest => dest.LastModifiedBy, src => "TESTE IMPLEMENTAÇÃO ALTERAR");

            config.NewConfig<Offer, OfferSummaryViewModel>()
                .Map(dest => dest.CategoryName, src => src.CategoryNavigation.Name);

            config.NewConfig<Offer, OfferDetailsViewModel>()
                .Map(dest => dest.CategoryName, src => src.CategoryNavigation.Name);

        }
    }
}
