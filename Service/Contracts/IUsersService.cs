using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetUsers();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetUsers(Func<User, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUser"></param>
        void InsertUser(User objUser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUser"></param>
        void InsertBulkUser(IEnumerable<User> objUser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUser"></param>
        void UpdateUser(User objUser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUser"></param>
        void DeleteUser(User objUser);


     

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<User> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<User> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
