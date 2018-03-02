﻿using DataAccess.BLL;
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
    public interface IDbFactory : IDisposable
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Entities Get();
    }
}