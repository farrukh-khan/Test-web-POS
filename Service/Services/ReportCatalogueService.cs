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
    public class ReportCatalogueService : IReportCatalogueService
    {
        private readonly IReportCatalogueRepository _roleRepository;
        private readonly IUnitofWork _unitOfWork;

        public ReportCatalogueService(IReportCatalogueRepository appsRepository, IUnitofWork unitOfWork)
        {
            _roleRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region IReportCataloguetService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportCatalogue> GetReportCatalogues()
        {
            return _roleRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportCatalogue> GetReportCatalogues(Func<ReportCatalogue, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _roleRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReportCatalogue GetReportCatalogueById(long id)
        {
            return _roleRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReportCatalogue"></param>
        public void InsertReportCatalogue(ReportCatalogue objReportCatalogue)
        {
            _roleRepository.Add(objReportCatalogue);
            _unitOfWork.Commit();
        }


        public void InsertBulkReportCatalogue(IEnumerable<ReportCatalogue> objReportCatalogue)
        {
            foreach (var item in objReportCatalogue)
            {
                _roleRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReportCatalogue"></param>
        public void UpdateReportCatalogue(ReportCatalogue objReportCatalogue)
        {
            _roleRepository.Update(objReportCatalogue);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReportCatalogue"></param>
        public void DeleteReportCatalogue(ReportCatalogue objReportCatalogue)
        {
            _roleRepository.Delete(objReportCatalogue);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<ReportCatalogue> ExcuteSpType(string query, SqlParameter[] Parameters)
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
        public IEnumerable<ReportCatalogue> GetBySqlEntity(string query)
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
