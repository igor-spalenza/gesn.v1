using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.ViewModels.Offer
{
    public record OfferSummaryViewModel(
            [property: Display(Name = "Offer_Id_label", Prompt = "Offer_Id_placeholder")]
            string Id,
            [property: Display(Name = "Offer_Name_label", Prompt = "Offer_Name_placeholder")]
            string Name,
            [property: Display(Name = "Offer_SKU_label", Prompt = "Offer_SKU_placeholder")]
            string SKU,
            [property: Display(Name = "Offer_FormattedPrice_label", Prompt = "Offer_FormattedPrice_placeholder")]
            [property: DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
            decimal Price,
            [property: Display(Name = "Offer_CategoryName_label", Prompt = "Offer_CategoryName_placeholder")]
            string CategoryName,
            [property: Display(Name = "Offer_IsActive_label", Prompt = "Offer_IsActive_placeholder")]
            bool IsActive,
            [property: Display(Name = "Offer_AssemblyTimeInfo_label", Prompt = "Offer_AssemblyTimeInfo_placeholder")]
            string AssemblyTimeInfo
        );
}

