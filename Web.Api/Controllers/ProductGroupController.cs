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
using System.Data.OleDb;
using Web.Api.Models.Product;
using Web.Api.Models.Invoice;

namespace Web.Api.Controllers
{
    public class ProductGroupController : ApiController
    {
        #region Fields


        private readonly IProductGroupService _productGroupService;
        private readonly IProductMapService _productMapService;

        private readonly ISpService _spService;
        private readonly ICompanyService _companyService;
        private static Excel.Workbook MyBook = null;
        private static Excel.Application MyApp = null;
        private static Excel.Worksheet MySheet = null;

        #endregion

        #region Ctor
        public ProductGroupController()
        {
            _spService = StructureMap.ObjectFactory.GetInstance<ISpService>();
            _companyService = StructureMap.ObjectFactory.GetInstance<ICompanyService>();
            _productGroupService = StructureMap.ObjectFactory.GetInstance<IProductGroupService>();
            _productMapService = StructureMap.ObjectFactory.GetInstance<IProductMapService>();
        }
        #endregion

        #region Api Controllers



        /// <summary>
        /// ProductGroup section start
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        [Authorize]
        [ActionName("GetProductGroups")]

        public IHttpActionResult GetProductGroups(ProductGroupSearchModel model)
        {
            try
            {
                var data = GetDataList(model).Tables[0].AsEnumerable().GroupBy(g => g.Field<long>("GroupId")).Select(s => new
                {
                    Id = s.Key,
                    Code = s.First().Field<string>("GroupCode"),
                    Name = s.First().Field<string>("GroupName"),
                    Description = s.First().Field<string>("Description"),
                    Price = s.First().Field<double>("GroupSRate"),
                    Products = s.Select(ss => new
                    {
                        Id = ss.Field<long>("TestId"),
                        Name = ss.Field<string>("TestName"),
                        Code = ss.Field<string>("TestCode")
                    }).ToList()

                }).ToList();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        private DataSet GetDataList(ProductGroupSearchModel model)
        {
            try
            {

                SqlParameter[] param = {
                                               new SqlParameter("@companyId",model.CompanyId),
                                               new SqlParameter("@id",model.Id),
                                               new SqlParameter("@search",model.Search),
                                       };

                return _spService.ExcuteSpAnonmious("[prc_getProductGroups]", param, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Authorize]
        [ActionName("GetProductGroupById")]

        public IHttpActionResult GetProductGroupById(int id, int? companyId)
        {
            try
            {
                //var data = _ProductGroupService.GetProductGroups(x => x.Id == id && x.CompanyId == companyId).FirstOrDefault();
                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPost]
        [Authorize]
        [ActionName("ProductGroupSubmit")]

        public IHttpActionResult ProductGroupSubmit(ProductGroupSearchModel model)
        {
            try
            {
                if (model.Id <= 0)
                {
                    var ProductGroupExist = _productGroupService.GetProductGroups(x => x.Name == model.Name && x.CompanyId == model.CompanyId).FirstOrDefault();

                    if (ProductGroupExist != null)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Group name already exist."));
                    }

                    var productGroup = new ProductGroup();
                    productGroup.Name = model.Name;
                    productGroup.Description = model.Description;
                    productGroup.SRate = model.SRate;
                    productGroup.Code = GetCode(model.CompanyId);
                    productGroup.IsActive = true;
                    productGroup.IsAdd = true;
                    productGroup.IsUpdate = true;
                    productGroup.IsDelete = true;
                    productGroup.IsContinue = true;
                    productGroup.CreatedDate = DateTime.Now;
                    productGroup.CompanyId = model.CompanyId;

                    _productGroupService.InsertProductGroup(productGroup);


                    foreach (var item in model.Products)
                    {
                        ProductMap map = new ProductMap()
                        {
                            ProductGroupId = productGroup.Id,
                            ProductId = item.Id
                        };

                        _productMapService.InsertProductMap(map);
                    }


                    string jsonData = JsonConvert.SerializeObject(productGroup);



                    SqlParameter[] param = {
                                               new SqlParameter("@logData",jsonData),
                                               new SqlParameter("@clientId",model.CompanyId),
                                               new SqlParameter("@userId",model.CreatedBy),
                                               new SqlParameter("@type","Insert"),
                                               new SqlParameter("@action","ProductGroupSubmit"),
                                               new SqlParameter("@controller","ProductGroup"),

                                       };

                    _spService.ExcuteSpAnonmious("prc_insertLog", param, 1);

                }
                else
                {
                    var productGroup = _productGroupService.GetProductGroupById(model.Id);

                    if (productGroup != null)
                    {

                        productGroup.Name = model.Name;
                        productGroup.Description = model.Description;
                        productGroup.SRate = model.SRate;
                        productGroup.ModifiedBy = model.CreatedBy;
                        productGroup.ModifiedDate = DateTime.Now;
                        _productGroupService.UpdateProductGroup(productGroup);



                        var maps = _productMapService.GetProductMaps(x => x.ProductGroupId == productGroup.Id).ToList();

                        foreach (var item in maps)
                        {
                            _productMapService.DeleteProductMap(item);
                        }
                        
                        foreach (var item in model.Products)
                        {
                            ProductMap map = new ProductMap()
                            {
                                ProductGroupId = productGroup.Id,
                                ProductId = item.Id
                            };

                            _productMapService.InsertProductMap(map);
                        }





                        string jsonData = JsonConvert.SerializeObject(model);

                        SqlParameter[] param = {
                                               new SqlParameter("@logData",jsonData),
                                               new SqlParameter("@clientId",model.CompanyId),
                                               new SqlParameter("@userId",model.CreatedBy),
                                               new SqlParameter("@type","Update"),
                                               new SqlParameter("@action","ProductGroupSubmit"),
                                               new SqlParameter("@controller","ProductGroup"),

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

                var last = _productGroupService.GetProductGroups(x => x.CompanyId == companyId).LastOrDefault();
                if (last == null)
                {
                    code = "D-01";
                }
                else
                {
                    code = string.Format("D-{0}", last.Code.Split('-')[1].Length > 1 ? (int.Parse(last.Code.Split('-')[1]) + 1).ToString() : "0" + (int.Parse(last.Code.Split('-')[1]) + 1).ToString());
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
        [ActionName("DeleteProductGroup")]
        public IHttpActionResult DeleteProductGroup(int id, int? CompanyId)
        {
            try
            {
                //var ProductGroups = _ProductGroupService.GetProductGroups(x => x.Id == id && x.CompanyId == CompanyId).FirstOrDefault();

                //if (ProductGroups == null)
                //{
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "ProductGroup does not exist."));
                //}

                //ProductGroups.IsActive = false;
                //ProductGroups.IsDelete = false;
                //_ProductGroupService.DeleteProductGroup(ProductGroups);

                //var roleList = _ProductGroupService.GetProductGroups(x => x.CompanyId == CompanyId).ToList();
                //return Ok(roleList);
                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }




        private IEnumerable<ProductGroup> GetFilterProductGroups(ProductGroupSearchModel model)
        {
            try
            {

                IEnumerable<ProductGroup> data = null;


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
