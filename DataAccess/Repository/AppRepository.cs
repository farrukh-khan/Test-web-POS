using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;

namespace DataAccess.Repository
{
    public class AppRepository : BaseRepository<App>, IAppRepository
    {
        public AppRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
