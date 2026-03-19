using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Models.ViewModels.Offer
{
    public record OfferHierarchySummaryViewModel(
        string Id,
        string Name,
        string Description,
        DateTime CreatedAt,
        string CreatedBy,
        EObjectState StateCode
    );
}
