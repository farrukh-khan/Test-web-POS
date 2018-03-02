using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Company> GetCompanys();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Company> GetCompanys(Func<Company, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Company GetCompanyById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCompany"></param>
        void InsertCompany(Company objCompany);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCompany"></param>
        void InsertBulkCompany(IEnumerable<Company> objCompany);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCompany"></param>
        void UpdateCompany(Company objCompany);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCompany"></param>
        void DeleteCompany(Company objCompany);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Company> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<Company> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
