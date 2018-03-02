using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> GetCategorys();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> GetCategorys(Func<Category, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Category GetCategoryById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCategory"></param>
        void InsertCategory(Category objCategory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCategory"></param>
        void InsertBulkCategory(IEnumerable<Category> objCategory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCategory"></param>
        void UpdateCategory(Category objCategory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCategory"></param>
        void DeleteCategory(Category objCategory);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Category> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<Category> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
