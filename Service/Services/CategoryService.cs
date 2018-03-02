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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _roleRepository;
        private readonly IUnitofWork _unitOfWork;

        public CategoryService(ICategoryRepository appsRepository, IUnitofWork unitOfWork)
        {
            _roleRepository = appsRepository;
            _unitOfWork = unitOfWork;
        }
        #region ICategorytService Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetCategorys()
        {
            return _roleRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetCategorys(Func<Category, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _roleRepository.GetMany(where);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetCategoryById(long id)
        {
            return _roleRepository.GetById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCategory"></param>
        public void InsertCategory(Category objCategory)
        {
            _roleRepository.Add(objCategory);
            _unitOfWork.Commit();
        }


        public void InsertBulkCategory(IEnumerable<Category> objCategory)
        {
            foreach (var item in objCategory)
            {
                _roleRepository.Add(item);
            }
            _unitOfWork.Commit();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCategory"></param>
        public void UpdateCategory(Category objCategory)
        {
            _roleRepository.Update(objCategory);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCategory"></param>
        public void DeleteCategory(Category objCategory)
        {
            _roleRepository.Delete(objCategory);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Category> ExcuteSpType(string query, SqlParameter[] Parameters)
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
        public IEnumerable<Category> GetBySqlEntity(string query)
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
