using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;

namespace DataAccess.Repository
{
    public class ReportCatalogueRepository : BaseRepository<ReportCatalogue>, IReportCatalogueRepository
    {
        public ReportCatalogueRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
