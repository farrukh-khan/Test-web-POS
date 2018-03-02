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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitofWork _unitOfWork;

        public RoleService(IRoleRepository appsRepository, IUnitofWork unitOfWork)
        {
            _roleRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region IRoletService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Role> GetRoles()
        {
            return _roleRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Role> GetRoles(Func<Role, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _roleRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRoleById(long id)
        {
            return _roleRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRole"></param>
        public void InsertRole(Role objRole)
        {
            _roleRepository.Add(objRole);
            _unitOfWork.Commit();
        }


        public void InsertBulkRole(IEnumerable<Role> objRole)
        {
            foreach (var item in objRole)
            {
                _roleRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRole"></param>
        public void UpdateRole(Role objRole)
        {
            _roleRepository.Update(objRole);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRole"></param>
        public void DeleteRole(Role objRole)
        {
            _roleRepository.Delete(objRole);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Role> ExcuteSpType(string query, SqlParameter[] Parameters)
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
        public IEnumerable<Role> GetBySqlEntity(string query)
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
