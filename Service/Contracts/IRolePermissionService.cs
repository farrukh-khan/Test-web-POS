using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IRolePermissionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<RolePermission> GetRolePermissions();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<RolePermission> GetRolePermissions(Func<RolePermission, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RolePermission GetRolePermissionById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRolePermission"></param>
        void InsertRolePermission(RolePermission objRolePermission);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRolePermission"></param>
        void InsertBulkRolePermission(IEnumerable<RolePermission> objRolePermission);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRolePermission"></param>
        void UpdateRolePermission(RolePermission objRolePermission);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRolePermission"></param>
        void DeleteRolePermission(RolePermission objRolePermission);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<RolePermission> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<RolePermission> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
