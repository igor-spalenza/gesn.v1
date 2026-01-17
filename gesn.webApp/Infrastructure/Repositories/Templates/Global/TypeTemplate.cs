using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Infrastructure.Repositories.Templates.Global
{
    public static class TypeTemplate
    {
        public static readonly QueryTemplate TypeSummaryTemplate = QueryTemplate.Create(
            wheres: new List<WhereTemplate> { WhereTemplate.Create($"T.StateCode == {(int)EObjectState.ACTIVE}", Models.Enums.Template.EWhereType.AND) },
            orderBy: "CreatedAt DESC"
        );
    }
}
