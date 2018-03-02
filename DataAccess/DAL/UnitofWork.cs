using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BLL;

namespace DataAccess.DAL
{
    public class UnitofWork : IUnitofWork
    {
        private readonly IDbFactory _dbFactory;
        private Entities _context;

        /// <summary>
        /// 
        /// </summary>s
        /// <param name="dbFactory"></param>
        public UnitofWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        protected Entities Context
        {
            get { return _context ?? (_context = _dbFactory.Get()); }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            Context.SaveChanges();            
        }
    }
}
