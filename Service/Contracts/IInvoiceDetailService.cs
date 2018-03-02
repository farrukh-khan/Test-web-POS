using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IInvoiceDetailService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<InvoiceDetail> GetInvoiceDetails();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<InvoiceDetail> GetInvoiceDetails(Func<InvoiceDetail, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        InvoiceDetail GetInvoiceDetailById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoiceDetail"></param>
        void InsertInvoiceDetail(InvoiceDetail objInvoiceDetail);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoiceDetail"></param>
        void InsertBulkInvoiceDetail(IEnumerable<InvoiceDetail> objInvoiceDetail);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoiceDetail"></param>
        void UpdateInvoiceDetail(InvoiceDetail objInvoiceDetail);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoiceDetail"></param>
        void DeleteInvoiceDetail(InvoiceDetail objInvoiceDetail);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<InvoiceDetail> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<InvoiceDetail> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
