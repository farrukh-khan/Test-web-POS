using DataAccess.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.DAL
{
    public class DbFactory : IDbFactory
    {
        private Entities _context;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Entities Get()
        {
            return _context ?? (_context = new Entities());
        }

        #region Dispose

        private bool _disposed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
