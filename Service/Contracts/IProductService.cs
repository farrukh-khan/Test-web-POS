using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> GetProducts();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> GetProducts(Func<Product, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProductById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProduct"></param>
        void InsertProduct(Product objProduct);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProduct"></param>
        void InsertBulkProduct(IEnumerable<Product> objProduct);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProduct"></param>
        void UpdateProduct(Product objProduct);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objProduct"></param>
        void DeleteProduct(Product objProduct);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Product> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<Product> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
