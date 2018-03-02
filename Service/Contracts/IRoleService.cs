using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IRoleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Role> GetRoles();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Role> GetRoles(Func<Role, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Role GetRoleById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRole"></param>
        void InsertRole(Role objRole);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRole"></param>
        void InsertBulkRole(IEnumerable<Role> objRole);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRole"></param>
        void UpdateRole(Role objRole);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRole"></param>
        void DeleteRole(Role objRole);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Role> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<Role> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
