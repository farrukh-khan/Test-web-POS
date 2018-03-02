using System;
using System.Collections.Generic;
using DataAccess.BLL;
using System.Collections;
using System.Data.SqlClient;

namespace Service.Contracts
{
    public interface IInvoiceService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Invoice> GetInvoices();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Invoice> GetInvoices(Func<Invoice, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Invoice GetInvoiceById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoice"></param>
        void InsertInvoice(Invoice objInvoice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoice"></param>
        void InsertBulkInvoice(IEnumerable<Invoice> objInvoice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoice"></param>
        void UpdateInvoice(Invoice objInvoice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoice"></param>
        void DeleteInvoice(Invoice objInvoice);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Invoice> ExcuteSpType(string query, SqlParameter[] Parameters);

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
        IEnumerable<Invoice> GetBySqlEntity(string query);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable GetBySql(string query);
        

        
    }
}
