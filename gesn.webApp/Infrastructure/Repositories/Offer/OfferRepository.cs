using gesn.webApp.Data.Repositories.Base;
using gesn.webApp.Interfaces.Data;
using gesn.webApp.Interfaces.Repositories.Offer;
using OfferEntity = gesn.webApp.Models.Entities.Offer.Offer;

namespace gesn.webApp.Infrastructure.Repositories.Offer
{
    public class OfferRepository : RepositoryBase<OfferEntity>, IOfferRepository
    {
        public OfferRepository(IDbConnectionFactory conn) : base(conn) { }
    }
}
