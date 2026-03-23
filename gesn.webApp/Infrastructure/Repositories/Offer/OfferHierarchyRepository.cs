using gesn.webApp.Data.Repositories.Base;
using gesn.webApp.Interfaces.Data;
using gesn.webApp.Interfaces.Repositories.Offer;
using gesn.webApp.Models.Entities.Offer;

namespace gesn.webApp.Infrastructure.Repositories.Offer
{
    public class OfferHierarchyRepository : RepositoryBase<OfferHierarchy>, IOfferHierarchyRepository
    {
        public OfferHierarchyRepository(IDbConnectionFactory conn) : base(conn) { }
    }
}
