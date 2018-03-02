using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.Repository;
using DataAccess.RepositoryContracts;

namespace DataAccess.Repositorys
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
