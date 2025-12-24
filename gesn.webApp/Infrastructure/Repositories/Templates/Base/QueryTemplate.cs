namespace gesn.webApp.Infrastructure.Repositories.Templates.Base
{
    public class QueryTemplate
    {
        public IList<WhereTemplate> Wheres { get; init; } = new List<WhereTemplate>();
        public IList<JoinTemplate> Joins { get; init; } = new List<JoinTemplate>();
        public string OrderBy { get; init; } = string.Empty;
        public string Select { get; init; } = "*";
        public string Having { get; init; } = string.Empty;
        public string GroupBy { get; init; } = string.Empty;
        public bool ApplySoftDelete { get; init; } = true;

        public static QueryTemplate Create(
            IList<WhereTemplate> wheres,
            string orderBy = "",
            string select = "*",
            string having = "",
            string groupBy = "",
            bool applySoftDelete = true,
            IList<JoinTemplate>? joins = null)
        {
            return new QueryTemplate
            {
                Wheres = wheres ?? new List<WhereTemplate>(),
                Joins = joins ?? new List<JoinTemplate>(),
                OrderBy = orderBy,
                Select = select,
                Having = having,
                GroupBy = groupBy,
                ApplySoftDelete = applySoftDelete
            };
        }
    }
}
