namespace gesn.webApp.Infrastructure.Repositories.Templates.Base
{
    public class QueryTemplate
    {
        public string Where { get; init; } = string.Empty;
        public string Joins { get; init; } = string.Empty;
        public string OrderBy { get; init; } = string.Empty;
        public string Select { get; init; } = "*";
        public string Having { get; init; } = string.Empty;
        public bool ApplySoftDelete { get; init; } = true;

        public static QueryTemplate Create(
            string where = "",
            string joins = "",
            string orderBy = "",
            string select = "*",
            string having = "",
            bool applySoftDelete = true)
        {
            return new QueryTemplate
            {
                Where = where,
                Joins = joins,
                OrderBy = orderBy,
                Select = select,
                Having = having,
                ApplySoftDelete = applySoftDelete
            };
        }
    }
}
