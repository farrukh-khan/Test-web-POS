using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    // <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>  
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Func<T, Boolean> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T Get(Func<T, Boolean> where);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        IEnumerable<T> GetAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<T> GetMany(Func<T, bool> where);



        
         /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<T> ExcuteSpType(string query, SqlParameter[] Parameters);
        IEnumerable<T> ExcuteSpType<T>(string query, SqlParameter[] Parameters);
        List<dynamic> ExcuteSpAnonmious<T>(string query, SqlParameter[] Parameters);

        DataSet ExcuteSpAnonmious(string query, SqlParameter[] Parameters, int dataTableCount);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable ExcuteSp(string query, SqlParameter[] Parameters);

        int ExcuteStoreProc(string query, SqlParameter[] Parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<T> GetBySqlEntity(string query);
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);

        
    }
}
