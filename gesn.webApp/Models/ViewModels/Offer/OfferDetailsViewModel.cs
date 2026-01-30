using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Models.ViewModels.Offer
{
    public record OfferDetailsViewModel
    (
         string Id,
         string Name,
         string? Description,
         string SKU,
         decimal Price,
         decimal UnitPrice,
         decimal Cost,
         string? CategoryName,
         string? Note,
         int AssemblyTime,
         EObjectState StateCode,
         DateTime CreatedAt,
         string CreatedBy,
         DateTime? LastModifiedAt,
         string? LastModifiedBy,
         int QuantityPrice
    );
}
