using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IActionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<DataAccess.BLL.Action> GetActions();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<DataAccess.BLL.Action> GetActions(Func<DataAccess.BLL.Action, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DataAccess.BLL.Action GetActionById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAction"></param>
        void InsertAction(DataAccess.BLL.Action objAction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAction"></param>
        void InsertBulkAction(IEnumerable<DataAccess.BLL.Action> objAction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAction"></param>
        void UpdateAction(DataAccess.BLL.Action objAction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAction"></param>
        void DeleteAction(DataAccess.BLL.Action objAction);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<DataAccess.BLL.Action> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<DataAccess.BLL.Action> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
