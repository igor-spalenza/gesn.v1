using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Models.ViewModels.Global
{
    public record TypeDetailsViewModel
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