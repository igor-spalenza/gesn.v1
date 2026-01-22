using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Models.ViewModels.Global
{
    public record CategoryDetailsViewModel
   (
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
