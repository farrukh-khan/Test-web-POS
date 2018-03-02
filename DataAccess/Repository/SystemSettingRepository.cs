using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;

namespace DataAccess.Repository
{
    public class SystemSettingRepository : BaseRepository<SystemSetting>, ISystemSettingRepository
    {
        public SystemSettingRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
