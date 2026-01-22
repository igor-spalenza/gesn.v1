using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Models.ViewModels.Global
{
    public class CategoryUpdateViewModel : CategoryBaseViewModel
    {
        public Guid Id { get; set; }
        public EObjectState StateCode { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
