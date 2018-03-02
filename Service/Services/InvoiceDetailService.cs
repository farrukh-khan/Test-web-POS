using System;
using System.Collections.Generic;
using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;
using Service.Contracts;
using System.Data.SqlClient;
using System.Collections;


namespace Service.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly IInvoiceDetailRepository _roleRepository;
        private readonly IUnitofWork _unitOfWork;

        public InvoiceDetailService(IInvoiceDetailRepository appsRepository, IUnitofWork unitOfWork)
        {
            _roleRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region IInvoiceDetailtService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InvoiceDetail> GetInvoiceDetails()
        {
            return _roleRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InvoiceDetail> GetInvoiceDetails(Func<InvoiceDetail, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _roleRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InvoiceDetail GetInvoiceDetailById(long id)
        {
            return _roleRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoiceDetail"></param>
        public void InsertInvoiceDetail(InvoiceDetail objInvoiceDetail)
        {
            _roleRepository.Add(objInvoiceDetail);
            _unitOfWork.Commit();
        }


        public void InsertBulkInvoiceDetail(IEnumerable<InvoiceDetail> objInvoiceDetail)
        {
            foreach (var item in objInvoiceDetail)
            {
                _roleRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoiceDetail"></param>
        public void UpdateInvoiceDetail(InvoiceDetail objInvoiceDetail)
        {
            _roleRepository.Update(objInvoiceDetail);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvoiceDetail"></param>
        public void DeleteInvoiceDetail(InvoiceDetail objInvoiceDetail)
        {
            _roleRepository.Delete(objInvoiceDetail);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceDetail> ExcuteSpType(string query, SqlParameter[] Parameters)
        {
            return _roleRepository.ExcuteSpType(query, Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable ExcuteSp(string query, SqlParameter[] Parameters)
        {
            return _roleRepository.ExcuteSp(query, Parameters);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceDetail> GetBySqlEntity(string query)
        {
            return _roleRepository.GetBySqlEntity(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable GetBySql(string query)
        {
            return _roleRepository.GetBySql(query);
        }

        #endregion

    }
}
