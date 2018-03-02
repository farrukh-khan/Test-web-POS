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
using Web.Api.Models.Product;
using System.Data.OleDb;

namespace Web.Api.Controllers
{
    public class ProductController : ApiController
    {
        #region Fields
        private readonly IProductService _ProductService;
        private readonly ISpService _spService;
        private readonly ICompanyService _companyService;

        private static Excel.Workbook MyBook = null;
        private static Excel.Application MyApp = null;
        private static Excel.Worksheet MySheet = null;

        #endregion

        #region Ctor
        public ProductController()
        {
            _ProductService = StructureMap.ObjectFactory.GetInstance<IProductService>();
            _spService = StructureMap.ObjectFactory.GetInstance<ISpService>();
            _companyService = StructureMap.ObjectFactory.GetInstance<ICompanyService>();

        }
        #endregion

        #region Api Controllers



        /// <summary>
        /// Product section start
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        [Authorize]
        [ActionName("GetProducts")]

        public IHttpActionResult GetProducts(ProductSearchModel model)
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


        private DataSet GetDataList(ProductSearchModel model)
        {
            try
            {

                SqlParameter[] param = {
                                               new SqlParameter("@companyId",model.CompanyId),
                                               new SqlParameter("@id",model.Id),
                                                new SqlParameter("@CurrentPage", model.page),
                                                new SqlParameter("@PageSize", model.pageSize),
                                       };

                return _spService.ExcuteSpAnonmious("prc_getProducts", param, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Authorize]
        [ActionName("GetProductById")]

        public IHttpActionResult GetProductById(int id, int? companyId)
        {
            try
            {
                var data = _ProductService.GetProducts(x => x.Id == id && x.CompanyId == companyId).FirstOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPost]
        [Authorize]
        [ActionName("ProductSubmit")]

        public IHttpActionResult ProductSubmit(ProductModel model)
        {
            try
            {
                if (model.Id <= 0)
                {
                    var Product = _ProductService.GetProducts(x => x.Name == model.Name && x.CompanyId == model.CompanyId).FirstOrDefault();

                    if (Product != null)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Test name already exist."));
                    }

                    var product = AutoMapper.Mapper.Map<ProductModel, Product>(model);
                    product.Code = GetCode(product.CompanyId);
                    product.IsActive = true;
                    product.IsAdd = false;
                    product.IsUpdate = true;
                    product.IsDelete = true;
                    product.IsContinue = true;
                    product.CreatedDate = DateTime.Now;
                    product.Type = "T";

                    _ProductService.InsertProduct(product);

                    string jsonData = JsonConvert.SerializeObject(product);



                    SqlParameter[] param = {
                                               new SqlParameter("@logData",jsonData),
                                               new SqlParameter("@clientId",model.CompanyId),
                                               new SqlParameter("@userId",model.CreatedBy),
                                               new SqlParameter("@type","Insert"),
                                               new SqlParameter("@action","ProductSubmit"),
                                               new SqlParameter("@controller","Product"),

                                       };

                    _spService.ExcuteSpAnonmious("prc_insertLog", param, 1);

                }
                else
                {
                    var product = _ProductService.GetProductById(model.Id);

                    if (product != null)
                    {

                        //string createdBy = model.CreatedBy;
                        //AutoMapper.Mapper.Map<ProductModel, Product>(model, Product);
                        product.Name = model.Name;
                        product.Description = model.Description;
                        product.SaleRate = model.SaleRate;
                        product.CategoryId = model.CategoryId;
                        _ProductService.UpdateProduct(product);

                        string jsonData = JsonConvert.SerializeObject(model);

                        SqlParameter[] param = {
                                               new SqlParameter("@logData",jsonData),
                                               new SqlParameter("@clientId",model.CompanyId),
                                               new SqlParameter("@userId",model.CreatedBy),
                                               new SqlParameter("@type","Update"),
                                               new SqlParameter("@action","ProductSubmit"),
                                               new SqlParameter("@controller","Product"),

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

        private string GetCode(long companyId)
        {
            try
            {
                string code = string.Empty;

                var last = _ProductService.GetProducts(x => x.CompanyId == companyId).LastOrDefault();
                if (last == null)
                {
                    code = "I-01";
                }
                else
                {
                    code = string.Format("I-{0}", last.Code.Split('-')[1].Length > 1 ? (int.Parse(last.Code.Split('-')[1]) + 1).ToString() : "0" + (int.Parse(last.Code.Split('-')[1]) + 1).ToString());
                }
                return code;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpGet]
        [Authorize]
        [ActionName("DeleteProduct")]
        public IHttpActionResult DeleteProduct(int id, int? CompanyId)
        {
            try
            {
                var Products = _ProductService.GetProducts(x => x.Id == id && x.CompanyId == CompanyId).FirstOrDefault();

                if (Products == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Product does not exist."));
                }

                Products.IsActive = false;
                Products.IsDelete = false;
                _ProductService.DeleteProduct(Products);

                var roleList = _ProductService.GetProducts(x => x.CompanyId == CompanyId).ToList();
                return Ok(roleList);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }










        private IEnumerable<Product> GetFilterProducts(ProductModel model)
        {
            try
            {

                IEnumerable<Product> data = null;


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
