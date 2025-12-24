using gesn.webApp.Data.Repositories.Base;
using gesn.webApp.Interfaces.Data;
using gesn.webApp.Interfaces.Services;
using gesn.webApp.Models.Entities.Offer;

namespace gesn.webApp.Infrastructure.Services
{
    public class TesteServices : RepositoryBase<Offer>, ITestesServices
    {
        public TesteServices(IDbConnectionFactory conn) : base(conn) { }

        // Implemente os métodos necessários, se houver
    }
}
