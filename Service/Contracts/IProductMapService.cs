using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IProductMapService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductMap> GetProductMaps();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductMap> GetProductMaps(Func<ProductMap, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductMap GetProductMapById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProductMap"></param>
        void InsertProductMap(ProductMap objProductMap);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProductMap"></param>
        void InsertBulkProductMap(IEnumerable<ProductMap> objProductMap);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProductMap"></param>
        void UpdateProductMap(ProductMap objProductMap);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProductMap"></param>
        void DeleteProductMap(ProductMap objProductMap);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<ProductMap> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<ProductMap> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
