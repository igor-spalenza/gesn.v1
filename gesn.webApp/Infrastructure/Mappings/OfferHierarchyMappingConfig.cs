using gesn.webApp.Models.Entities.Offer;
using gesn.webApp.Models.ViewModels.Offer;
using Mapster;

namespace gesn.webApp.Infrastructure.Mappings
{
    public class OfferHierarchyMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OfferHierarchyInsertViewModel, OfferHierarchy>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.StateCode)
                .Map(dest => dest.Id, src => new Guid().ToString());

            config.NewConfig<OfferHierarchyUpdateViewModel, OfferHierarchy>()
                .IgnoreNullValues(false)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.CreatedBy)
                .Ignore(dest => dest.StateCode)
                .Map(dest => dest.LastModifiedAt, src => DateTime.UtcNow)
                .Map(dest => dest.LastModifiedBy, src => src.LastModifiedBy);
        }
    }
}
