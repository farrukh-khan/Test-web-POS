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
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitofWork _unitOfWork;

        public RolePermissionService(IRolePermissionRepository appsRepository, IUnitofWork unitOfWork)
        {
            _rolePermissionRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region IRolePermissiontService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RolePermission> GetRolePermissions()
        {
            return _rolePermissionRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RolePermission> GetRolePermissions(Func<RolePermission, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _rolePermissionRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RolePermission GetRolePermissionById(long id)
        {
            return _rolePermissionRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRolePermission"></param>
        public void InsertRolePermission(RolePermission objRolePermission)
        {
            _rolePermissionRepository.Add(objRolePermission);
            _unitOfWork.Commit();
        }


        public void InsertBulkRolePermission(IEnumerable<RolePermission> objRolePermission)
        {
            foreach (var item in objRolePermission)
            {
                _rolePermissionRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRolePermission"></param>
        public void UpdateRolePermission(RolePermission objRolePermission)
        {
            _rolePermissionRepository.Update(objRolePermission);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRolePermission"></param>
        public void DeleteRolePermission(RolePermission objRolePermission)
        {
            _rolePermissionRepository.Delete(objRolePermission);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<RolePermission> ExcuteSpType(string query, SqlParameter[] Parameters)
        {
            return _rolePermissionRepository.ExcuteSpType(query, Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable ExcuteSp(string query, SqlParameter[] Parameters)
        {
            return _rolePermissionRepository.ExcuteSp(query, Parameters);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<RolePermission> GetBySqlEntity(string query)
        {
            return _rolePermissionRepository.GetBySqlEntity(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable GetBySql(string query)
        {
            return _rolePermissionRepository.GetBySql(query);
        }

        #endregion

    }
}
