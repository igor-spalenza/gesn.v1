using gesn.webApp.Models.Enums.Template;

namespace gesn.webApp.Infrastructure.Repositories.Templates.Base
{
    public class WhereTemplate
    {
        public string Statement = string.Empty;
        public EWhereType Type = EWhereType.AND;

        public static WhereTemplate Create(string statement, EWhereType type = EWhereType.AND)
        {
            return new WhereTemplate
            {
                Statement = statement,
                Type = type
            };
        }
    }
}