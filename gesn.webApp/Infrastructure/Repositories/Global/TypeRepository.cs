using gesn.webApp.Data.Repositories.Base;
using gesn.webApp.Interfaces.Data;
using gesn.webApp.Interfaces.Repositories.Global;

namespace gesn.webApp.Infrastructure.Repositories.Offer
{
    public class TypeRepository : RepositoryBase<Models.Entities.Global.Type>, ITypeRepository
    {
        public TypeRepository(IDbConnectionFactory conn) : base(conn) { }
    }
}
