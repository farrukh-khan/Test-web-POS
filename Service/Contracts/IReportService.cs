using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IReportService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Report> GetReports();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Report> GetReports(Func<Report, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Report GetReportById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReport"></param>
        void InsertReport(Report objReport);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReport"></param>
        void InsertBulkReport(IEnumerable<Report> objReport);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReport"></param>
        void UpdateReport(Report objReport);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReport"></param>
        void DeleteReport(Report objReport);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Report> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<Report> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
