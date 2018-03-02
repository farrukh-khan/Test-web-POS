using System;
using System.Collections.Generic;
using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;
using System.Data.SqlClient;
using Service.Contracts;
using System.Collections;

namespace Service.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _systemUserRepository;
        private readonly IUnitofWork _unitOfWork;

        public UserService(IUserRepository systemUserRepository, IUnitofWork unitOfWork)
        {
            _systemUserRepository = systemUserRepository;
            _unitOfWork = unitOfWork;
        }
        #region IUsertService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers()
        {
            return _systemUserRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers(Func<User, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _systemUserRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(long id)
        {
            return _systemUserRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUser"></param>
        public void InsertUser(User objUser)
        {
            _systemUserRepository.Add(objUser);
            _unitOfWork.Commit();
        }


        public void InsertBulkUser(IEnumerable<User> objUser)
        {
            foreach (var item in objUser)
            {
                _systemUserRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUser"></param>
        public void UpdateUser(User objUser)
        {
            _systemUserRepository.Update(objUser);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUser"></param>
        public void DeleteUser(User objUser)
        {
            _systemUserRepository.Delete(objUser);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<User> ExcuteSpType(string query, SqlParameter[] Parameters)
        {
            return _systemUserRepository.ExcuteSpType(query, Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable ExcuteSp(string query, SqlParameter[] Parameters)
        {
            return _systemUserRepository.ExcuteSp(query, Parameters);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<User> GetBySqlEntity(string query)
        {
            return _systemUserRepository.GetBySqlEntity(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable GetBySql(string query)
        {
            return _systemUserRepository.GetBySql(query);
        }

        #endregion

    }
}
