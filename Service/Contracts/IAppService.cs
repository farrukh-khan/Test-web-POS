using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<App> GetApps();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<App> GetApps(Func<App, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        App GetAppById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objApp"></param>
        void InsertApp(App objApp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objApp"></param>
        void InsertBulkApp(IEnumerable<App> objApp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objApp"></param>
        void UpdateApp(App objApp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objApp"></param>
        void DeleteApp(App objApp);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<App> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<App> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
