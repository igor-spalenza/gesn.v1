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
                .Map(dest => dest.CategoryId, src => src.CategoryId.ToString())
                .Map(dest => dest.Id, src => src.Id.ToString())
                .Map(dest => dest.SKU, src => src.SKU)
                .Map(dest => dest.ImageUrl, src => src.ImageUrl ?? string.Empty)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.CreatedBy, src => src.CreatedBy)
                .Map(dest => dest.LastModifiedAt, src => DateTime.Now)
                .Map(dest => dest.LastModifiedBy, src => "TESTE IMPLEMENTAÇÃO ALTERAR")
                .Map(dest => dest.AssemblyInstructions, src => src.AssemblyInstructions)
                .Map(dest => dest.AssemblyTime, src => src.AssemblyTime)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Note, src => src.Note)
                .Map(dest => dest.Cost, src => src.Cost)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.QuantityPrice, src => src.QuantityPrice)
                .Map(dest => dest.UnitPrice, src => src.UnitPrice)
                ;

            config.NewConfig<Offer, OfferSummaryViewModel>()
                .Map(dest => dest.CategoryName, src => src.CategoryNavigation.Name);

            config.NewConfig<Offer, OfferDetailsViewModel>()
                .Map(dest => dest.CategoryName, src => src.CategoryNavigation.Name);

        }
    }
}
