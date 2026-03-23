using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Models.ViewModels.Offer
{
    public record OfferHierarchyDetailsViewModel(
        string Id,
        string Name,
        string Description,
        DateTime CreatedAt,
        string CreatedBy,
        DateTime LastModifiedAt,
        string LastModifiedBy,
        EObjectState StateCode
    );
}
