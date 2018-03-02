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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _roleRepository;
        private readonly IUnitofWork _unitOfWork;

        public CompanyService(ICompanyRepository appsRepository, IUnitofWork unitOfWork)
        {
            _roleRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region ICompanytService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Company> GetCompanys()
        {
            return _roleRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Company> GetCompanys(Func<Company, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _roleRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Company GetCompanyById(long id)
        {
            return _roleRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCompany"></param>
        public void InsertCompany(Company objCompany)
        {
            _roleRepository.Add(objCompany);
            _unitOfWork.Commit();
        }


        public void InsertBulkCompany(IEnumerable<Company> objCompany)
        {
            foreach (var item in objCompany)
            {
                _roleRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCompany"></param>
        public void UpdateCompany(Company objCompany)
        {
            _roleRepository.Update(objCompany);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCompany"></param>
        public void DeleteCompany(Company objCompany)
        {
            _roleRepository.Delete(objCompany);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Company> ExcuteSpType(string query, SqlParameter[] Parameters)
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
        public IEnumerable<Company> GetBySqlEntity(string query)
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
