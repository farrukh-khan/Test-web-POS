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
    public class AppService : IAppService
    {
        private readonly IAppRepository _appsRepository;
        private readonly IUnitofWork _unitOfWork;

        public AppService(IAppRepository appsRepository, IUnitofWork unitOfWork)
        {
            _appsRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region IApptService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<App> GetApps()
        {
            return _appsRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<App> GetApps(Func<App, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _appsRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public App GetAppById(long id)
        {
            return _appsRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objApp"></param>
        public void InsertApp(App objApp)
        {
            _appsRepository.Add(objApp);
            _unitOfWork.Commit();
        }


        public void InsertBulkApp(IEnumerable<App> objApp)
        {
            foreach (var item in objApp)
            {
                _appsRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objApp"></param>
        public void UpdateApp(App objApp)
        {
            _appsRepository.Update(objApp);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objApp"></param>
        public void DeleteApp(App objApp)
        {
            _appsRepository.Delete(objApp);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<App> ExcuteSpType(string query, SqlParameter[] Parameters)
        {
            return _appsRepository.ExcuteSpType(query, Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable ExcuteSp(string query, SqlParameter[] Parameters)
        {
            return _appsRepository.ExcuteSp(query, Parameters);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<App> GetBySqlEntity(string query)
        {
            return _appsRepository.GetBySqlEntity(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable GetBySql(string query)
        {
            return _appsRepository.GetBySql(query);
        }

        #endregion

    }
}
