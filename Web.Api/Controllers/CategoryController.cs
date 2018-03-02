using DataAccess.BLL;
using Newtonsoft.Json;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Web.Api.Common;

using System.Security.Principal;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using Web.Api.Models.Category;
using System.Data.OleDb;

namespace Web.Api.Controllers
{
    public class CategoryController : ApiController
    {
        #region Fields
        private readonly ICategoryService _CategoryService;
        private readonly ISpService _spService;
        private readonly ICompanyService _companyService;

        private static Excel.Workbook MyBook = null;
        private static Excel.Application MyApp = null;
        private static Excel.Worksheet MySheet = null;

        #endregion

        #region Ctor
        public CategoryController()
        {
            _CategoryService = StructureMap.ObjectFactory.GetInstance<ICategoryService>();
            _spService = StructureMap.ObjectFactory.GetInstance<ISpService>();
            _companyService = StructureMap.ObjectFactory.GetInstance<ICompanyService>();

        }
        #endregion

        #region Api Controllers



        /// <summary>
        /// Category section start
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        [Authorize]
        [ActionName("GetCategorys")]

        public IHttpActionResult GetCategorys(CategorySearchModel model)
        {
            try
            {
                return Ok(GetDataList(model));
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        private DataSet GetDataList(CategorySearchModel model)
        {
            try
            {

                SqlParameter[] param = {
                                               new SqlParameter("@companyId",model.CompanyId),
                                               new SqlParameter("@id",model.Id),
                                                new SqlParameter("@CurrentPage", model.page),
                                                new SqlParameter("@PageSize", model.pageSize),
                                       };

                var data  = _spService.ExcuteSpAnonmious("prc_getCategorys", param, 2); ;

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Authorize]
        [ActionName("GetCategoryById")]

        public IHttpActionResult GetCategoryById(int id, int? companyId)
        {
            try
            {
                var data = _CategoryService.GetCategorys(x => x.Id == id && x.CompanyId == companyId).FirstOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPost]
        [Authorize]
        [ActionName("CategorySubmit")]

        public IHttpActionResult CategorySubmit(CategoryModel model)
        {
            try
            {
                if (model.Id <= 0)
                {
                    var Category = _CategoryService.GetCategorys(x => x.Name == model.Name && x.CompanyId == model.CompanyId).FirstOrDefault();

                    if (Category != null)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Test name already exist."));
                    }

                    var category = AutoMapper.Mapper.Map<CategoryModel, Category>(model);
                    category.IsActive = true;
                    category.IsAdd = false;
                    category.IsUpdate = true;
                    category.IsDelete = true;
                    category.IsContinue = true;
                    category.CreatedDate = DateTime.Now;
                    _CategoryService.InsertCategory(category);

                    string jsonData = JsonConvert.SerializeObject(Category);



                    SqlParameter[] param = {
                                               new SqlParameter("@logData",jsonData),
                                               new SqlParameter("@clientId",model.CompanyId),
                                               new SqlParameter("@userId",model.CreatedBy),
                                               new SqlParameter("@type","Insert"),
                                               new SqlParameter("@action","CategorySubmit"),
                                               new SqlParameter("@controller","Category"),

                                       };

                    _spService.ExcuteSpAnonmious("prc_insertLog", param, 1);

                }
                else
                {
                    var Category = _CategoryService.GetCategoryById(model.Id);

                    if (Category != null)
                    {

                        //string createdBy = model.CreatedBy;
                        //AutoMapper.Mapper.Map<CategoryModel, Category>(model, Category);
                        Category.Name = model.Name;
                        _CategoryService.UpdateCategory(Category);

                        string jsonData = JsonConvert.SerializeObject(Category);

                        SqlParameter[] param = {
                                               new SqlParameter("@logData",jsonData),
                                               new SqlParameter("@clientId",model.CompanyId),
                                               new SqlParameter("@userId",model.CreatedBy),
                                               new SqlParameter("@type","Update"),
                                               new SqlParameter("@action","CategorySubmit"),
                                               new SqlParameter("@controller","Category"),

                                       };

                        _spService.ExcuteSpAnonmious("prc_insertLog", param, 1);





                    }
                    else
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Test does not exist, please try again!"));
                    }


                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }




        [HttpGet]
        [Authorize]
        [ActionName("DeleteCategory")]
        public IHttpActionResult DeleteCategory(int id, int? CompanyId)
        {
            try
            {
                var Categorys = _CategoryService.GetCategorys(x => x.Id == id && x.CompanyId == CompanyId).FirstOrDefault();

                if (Categorys == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Category does not exist."));
                }

                Categorys.IsActive = false;
                Categorys.IsDelete = false;
                _CategoryService.DeleteCategory(Categorys);

                var roleList = _CategoryService.GetCategorys(x => x.CompanyId == CompanyId).ToList();
                return Ok(roleList);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        private IEnumerable<Category> GetFilterCategorys(CategoryModel model)
        {
            try
            {

                IEnumerable<Category> data = null;


                return data;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        #endregion
    }
}
