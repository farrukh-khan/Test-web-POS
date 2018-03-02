using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;

namespace DataAccess.Repository
{
    public class ActionRepository : BaseRepository<Action>, IActionRepository
    {
        public ActionRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
