using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Models.ViewModels.Offer.Category
{
    public class CategoryUpdateVM : CategoryBaseVM
    {
        public EObjectState StateCode { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
