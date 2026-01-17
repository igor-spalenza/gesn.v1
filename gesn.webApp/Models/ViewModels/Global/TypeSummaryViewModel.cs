using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Models.ViewModels.Global
{
    public record TypeSummaryViewModel(
        string Id,
        string Name,
        string Description,
        DateTime CreatedAt,
        string CreatedBy,
        EObjectState StateCode
    );
}
