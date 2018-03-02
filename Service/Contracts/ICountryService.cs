using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface ICountryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Country> GetCountrys();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Country> GetCountrys(Func<Country, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Country GetCountryById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCountry"></param>
        void InsertCountry(Country objCountry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCountry"></param>
        void InsertBulkCountry(IEnumerable<Country> objCountry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCountry"></param>
        void UpdateCountry(Country objCountry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCountry"></param>
        void DeleteCountry(Country objCountry);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Country> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<Country> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
