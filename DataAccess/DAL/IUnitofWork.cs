using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitofWork
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        void Commit();
    }
}
