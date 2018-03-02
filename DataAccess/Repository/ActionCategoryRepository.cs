using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;

namespace DataAccess.Repository
{
    public class ActionCategoryRepository : BaseRepository<ActionCategory>, IActionCategoryRepository
    {
        public ActionCategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
