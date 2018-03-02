using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IReportCatalogueService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ReportCatalogue> GetReportCatalogues();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ReportCatalogue> GetReportCatalogues(Func<ReportCatalogue, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ReportCatalogue GetReportCatalogueById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReportCatalogue"></param>
        void InsertReportCatalogue(ReportCatalogue objReportCatalogue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReportCatalogue"></param>
        void InsertBulkReportCatalogue(IEnumerable<ReportCatalogue> objReportCatalogue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReportCatalogue"></param>
        void UpdateReportCatalogue(ReportCatalogue objReportCatalogue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReportCatalogue"></param>
        void DeleteReportCatalogue(ReportCatalogue objReportCatalogue);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<ReportCatalogue> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<ReportCatalogue> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
