using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Models.Enums.Template;

namespace gesn.webApp.Infrastructure.Repositories.Templates.Offer
{
    public static class OfferTemplates
    {
        public static readonly QueryTemplate TesteTemplate = QueryTemplate.Create(
            wheres: new List<WhereTemplate> { WhereTemplate.Create("O.UnitPrice > 0") },
            joins: new List<JoinTemplate> { JoinTemplate.Create("Category C ON C.Id = O.CategoryId", EJoinType.LEFT) },
            orderBy: "O.CreatedAt DESC",
            select: "O.Id, O.Name"
        );

        public static readonly QueryTemplate SummaryTemplate = QueryTemplate.Create(
            wheres: new List<WhereTemplate> { WhereTemplate.Create("o.StateCode = 1") },
            joins: new List<JoinTemplate> { JoinTemplate.Create("Category c ON o.CategoryId == c.Id", EJoinType.LEFT) },
            orderBy: "o.CreatedAt DESC",
            select: "o.*, c.Id, c.Name"
        );

        public static readonly QueryTemplate GetByIdTemplate = QueryTemplate.Create(
            wheres: new List<WhereTemplate> { WhereTemplate.Create("o.Id == @id") },
            joins: new List<JoinTemplate> { JoinTemplate.Create("Category c ON o.CategoryId == c.Id", EJoinType.LEFT) },
            orderBy: "o.CreatedAt DESC",
            select: "o.*, c.Id, c.Name"
        );
    }
}