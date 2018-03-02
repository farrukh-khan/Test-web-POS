using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IActionCategoryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ActionCategory> GetActionCategorys();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ActionCategory> GetActionCategorys(Func<ActionCategory, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionCategory GetActionCategoryById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objActionCategory"></param>
        void InsertActionCategory(ActionCategory objActionCategory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objActionCategory"></param>
        void InsertBulkActionCategory(IEnumerable<ActionCategory> objActionCategory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objActionCategory"></param>
        void UpdateActionCategory(ActionCategory objActionCategory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objActionCategory"></param>
        void DeleteActionCategory(ActionCategory objActionCategory);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<ActionCategory> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<ActionCategory> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
