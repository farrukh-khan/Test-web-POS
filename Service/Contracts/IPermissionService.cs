using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IPermissionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Permission> GetPermissions();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Permission> GetPermissions(Func<Permission, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Permission GetPermissionById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPermission"></param>
        void InsertPermission(Permission objPermission);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPermission"></param>
        void InsertBulkPermission(IEnumerable<Permission> objPermission);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPermission"></param>
        void UpdatePermission(Permission objPermission);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPermission"></param>
        void DeletePermission(Permission objPermission);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Permission> ExcuteSpType(string query, SqlParameter[] Parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable ExcuteSp(string query, SqlParameter[] Parameters);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Permission> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
