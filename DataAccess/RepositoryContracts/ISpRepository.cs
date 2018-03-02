using DataAccess.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoryContracts
{
    public interface ISpRepository : IRepository<IEnumerable>
    {
    }
    
}
