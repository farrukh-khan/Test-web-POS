using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;

namespace DataAccess.Repository
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
