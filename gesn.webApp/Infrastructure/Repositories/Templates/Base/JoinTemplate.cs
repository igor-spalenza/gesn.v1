using gesn.webApp.Models.Enums.Template;

namespace gesn.webApp.Infrastructure.Repositories.Templates.Base
{
    public class JoinTemplate
    {
        public string Statement = string.Empty;
        public EJoinType Type = EJoinType.INNER;

        public static JoinTemplate Create(string statement, EJoinType type = EJoinType.INNER)
        {
            return new JoinTemplate
            {
                Statement = statement,
                Type = type
            };
        }
    }
}