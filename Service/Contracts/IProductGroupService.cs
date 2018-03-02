using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IProductGroupService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductGroup> GetProductGroups();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductGroup> GetProductGroups(Func<ProductGroup, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductGroup GetProductGroupById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProductGroup"></param>
        void InsertProductGroup(ProductGroup objProductGroup);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProductGroup"></param>
        void InsertBulkProductGroup(IEnumerable<ProductGroup> objProductGroup);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProductGroup"></param>
        void UpdateProductGroup(ProductGroup objProductGroup);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProductGroup"></param>
        void DeleteProductGroup(ProductGroup objProductGroup);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<ProductGroup> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<ProductGroup> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
