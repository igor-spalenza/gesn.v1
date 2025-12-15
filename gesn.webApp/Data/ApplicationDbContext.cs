using gesn.webApp.Infrastructure.Configuration;

namespace gesn.webApp.Data
{
    public class ApplicationDbContext : ProjectDataContext
    {
        public ApplicationDbContext(string connectionString) : base(connectionString) { }
    }
}
