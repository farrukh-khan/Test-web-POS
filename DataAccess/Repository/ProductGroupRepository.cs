using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;

namespace DataAccess.Repository
{
    public class ProductGroupRepository : BaseRepository<ProductGroup>, IProductGroupRepository
    {
        public ProductGroupRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
