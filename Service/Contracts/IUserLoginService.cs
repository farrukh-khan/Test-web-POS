using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IUserLoginService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserLogin> GetUserLogins();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserLogin> GetUserLogins(Func<UserLogin, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserLogin GetUserLoginById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUserLogin"></param>
        void InsertUserLogin(UserLogin objUserLogin);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUserLogin"></param>
        void InsertBulkUserLogin(IEnumerable<UserLogin> objUserLogin);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUserLogin"></param>
        void UpdateUserLogin(UserLogin objUserLogin);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUserLogin"></param>
        void DeleteUserLogin(UserLogin objUserLogin);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<UserLogin> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<UserLogin> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
