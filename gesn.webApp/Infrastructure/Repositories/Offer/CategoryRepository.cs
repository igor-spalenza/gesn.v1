using gesn.webApp.Data.Repositories.Base;
using gesn.webApp.Interfaces.Data;
using gesn.webApp.Interfaces.Repositories.Offer;
using gesn.webApp.Models.Entities.Global;

namespace gesn.webApp.Infrastructure.Repositories.Offer
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbConnectionFactory conn) : base(conn) { }
    }
}
