using gesn.webApp.Models.Entities.Global;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.ViewModels.Offer
{
    public class OfferBaseInsertViewModel
    {
        [Display(Name = "Offer_Name_label", Prompt = "Offer_Description_placeholder")]
        public string Name { get; set; } = string.Empty;
      
        [Display(Name = "Offer_Description_label", Prompt = "Offer_Description_placeholder")]
        public string? Description { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Offer_Price_label", Prompt = "Offer_Price_placeholder")]
        public decimal Price { get; set; }
      
        [Display(Name = "Offer_QuantityPrice_label", Prompt = "Offer_QuantityPrice_placeholder")]
        public int QuantityPrice { get; set; }
      
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Offer_UnitPrice_label", Prompt = "Offer_UnitPrice_placeholder")]
        public decimal UnitPrice { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Offer_Cost_label", Prompt = "Offer_Cost_placeholder")]
        public decimal Cost { get; set; }
        
        [Display(Name = "Offer_CategoryId_label", Prompt = "Offer_CategoryId_placeholder")]
        public string? CategoryId { get; set; }
        
        [Display(Name = "Offer_SKU_label", Prompt = "Offer_SKU_placeholder")]
        public string SKU { get; set; } = string.Empty;
       
        [Display(Name = "Offer_ImageUrl_label", Prompt = "Offer_ImageUrl_placeholder")]
        public string ImageUrl { get; set; } = string.Empty;
        
        [Display(Name = "Offer_Note_label", Prompt = "Offer_Note_placeholder")]
        public string? Note { get; set; }
       
        [Display(Name = "Offer_AssemblyTime_label", Prompt = "Offer_AssemblyTime_placeholder")]
        public int AssemblyTime { get; set; }
        
        [Display(Name = "Offer_AssemblyInformation_label", Prompt = "Offer_AssemblyInformation_placeholder")]
        public string AssemblyInformation { get; set; } = string.Empty;
        
        [Display(Name = "Offer_CategoryNavigation_label", Prompt = "Offer_CategoryNavigation_placeholder")]
        public Category? CategoryNavigation { get; set; }
       
        [Display(Name = "Offer_Categories_label", Prompt = "Offer_Categories_placeholder")]
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
