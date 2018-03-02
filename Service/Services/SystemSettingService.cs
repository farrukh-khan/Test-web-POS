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
    public class SystemSettingService : ISystemSettingService
    {
        private readonly ISystemSettingRepository _roleRepository;
        private readonly IUnitofWork _unitOfWork;

        public SystemSettingService(ISystemSettingRepository appsRepository, IUnitofWork unitOfWork)
        {
            _roleRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region ISystemSettingtService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SystemSetting> GetSystemSettings()
        {
            return _roleRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SystemSetting> GetSystemSettings(Func<SystemSetting, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _roleRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemSetting GetSystemSettingById(long id)
        {
            return _roleRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSystemSetting"></param>
        public void InsertSystemSetting(SystemSetting objSystemSetting)
        {
            _roleRepository.Add(objSystemSetting);
            _unitOfWork.Commit();
        }


        public void InsertBulkSystemSetting(IEnumerable<SystemSetting> objSystemSetting)
        {
            foreach (var item in objSystemSetting)
            {
                _roleRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSystemSetting"></param>
        public void UpdateSystemSetting(SystemSetting objSystemSetting)
        {
            _roleRepository.Update(objSystemSetting);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSystemSetting"></param>
        public void DeleteSystemSetting(SystemSetting objSystemSetting)
        {
            _roleRepository.Delete(objSystemSetting);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<SystemSetting> ExcuteSpType(string query, SqlParameter[] Parameters)
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
        public IEnumerable<SystemSetting> GetBySqlEntity(string query)
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
