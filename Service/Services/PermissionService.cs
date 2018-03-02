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
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitofWork _unitOfWork;

        public PermissionService(IPermissionRepository appsRepository, IUnitofWork unitOfWork)
        {
            _permissionRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region IPermissiontService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Permission> GetPermissions()
        {
            return _permissionRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Permission> GetPermissions(Func<Permission, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _permissionRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Permission GetPermissionById(long id)
        {
            return _permissionRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPermission"></param>
        public void InsertPermission(Permission objPermission)
        {
            _permissionRepository.Add(objPermission);
            _unitOfWork.Commit();
        }


        public void InsertBulkPermission(IEnumerable<Permission> objPermission)
        {
            foreach (var item in objPermission)
            {
                _permissionRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPermission"></param>
        public void UpdatePermission(Permission objPermission)
        {
            _permissionRepository.Update(objPermission);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPermission"></param>
        public void DeletePermission(Permission objPermission)
        {
            _permissionRepository.Delete(objPermission);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Permission> ExcuteSpType(string query, SqlParameter[] Parameters)
        {
            return _permissionRepository.ExcuteSpType(query, Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable ExcuteSp(string query, SqlParameter[] Parameters)
        {
            return _permissionRepository.ExcuteSp(query, Parameters);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Permission> GetBySqlEntity(string query)
        {
            return _permissionRepository.GetBySqlEntity(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable GetBySql(string query)
        {
            return _permissionRepository.GetBySql(query);
        }

        #endregion

    }
}
