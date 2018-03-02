using DataAccess.DAL;
using DataAccess.RepositoryContracts;
using DataAccess.RepositoryContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{

    public class SpRepository : BaseRepository<IEnumerable>, ISpRepository
    {
        public SpRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
   
}
  