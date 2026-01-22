using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Infrastructure.Repositories.Templates.Global
{
    public static class CategoryTemplate
    {
        public static readonly QueryTemplate CategorySummaryTemplate = QueryTemplate.Create(
            wheres: new List<WhereTemplate> { WhereTemplate.Create($"C.StateCode == {(int)EObjectState.ACTIVE}", Models.Enums.Template.EWhereType.AND) },
            orderBy: "C.CreatedAt DESC"
        );
    }
}
