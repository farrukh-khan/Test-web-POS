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
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserLoginRepository _userLoginRepository;
        private readonly IUnitofWork _unitOfWork;

        public UserLoginService(IUserLoginRepository appsRepository, IUnitofWork unitOfWork)
        {
            _userLoginRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region IUserLogintService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserLogin> GetUserLogins()
        {
            return _userLoginRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserLogin> GetUserLogins(Func<UserLogin, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _userLoginRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserLogin GetUserLoginById(long id)
        {
            return _userLoginRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUserLogin"></param>
        public void InsertUserLogin(UserLogin objUserLogin)
        {
            _userLoginRepository.Add(objUserLogin);
            _unitOfWork.Commit();
        }


        public void InsertBulkUserLogin(IEnumerable<UserLogin> objUserLogin)
        {
            foreach (var item in objUserLogin)
            {
                _userLoginRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUserLogin"></param>
        public void UpdateUserLogin(UserLogin objUserLogin)
        {
            _userLoginRepository.Update(objUserLogin);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUserLogin"></param>
        public void DeleteUserLogin(UserLogin objUserLogin)
        {
            _userLoginRepository.Delete(objUserLogin);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<UserLogin> ExcuteSpType(string query, SqlParameter[] Parameters)
        {
            return _userLoginRepository.ExcuteSpType(query, Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable ExcuteSp(string query, SqlParameter[] Parameters)
        {
            return _userLoginRepository.ExcuteSp(query, Parameters);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<UserLogin> GetBySqlEntity(string query)
        {
            return _userLoginRepository.GetBySqlEntity(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable GetBySql(string query)
        {
            return _userLoginRepository.GetBySql(query);
        }

        #endregion

    }
}
