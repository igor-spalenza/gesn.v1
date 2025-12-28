using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Models.Enums.Template;

namespace gesn.webApp.Infrastructure.Repositories.Templates
{
    public static class OfferTemplates
    {
        public static readonly QueryTemplate TesteTemplate = QueryTemplate.Create(
            wheres: new List<WhereTemplate> { WhereTemplate.Create("O.UnitPrice > @valor") },
            joins: new List<JoinTemplate> { JoinTemplate.Create("Category C ON C.Id = O.CategoryId", EJoinType.LEFT) },
            orderBy: "CreatedAt DESC",
            select: "O.Id, O.Name"
        );
    }
}