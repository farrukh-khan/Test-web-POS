using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;

namespace DataAccess.Repository
{
    public class ProductMapRepository : BaseRepository<ProductMap>, IProductMapRepository
    {
        public ProductMapRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
