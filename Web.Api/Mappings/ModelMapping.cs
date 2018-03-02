using System;
using System.Web.Mvc;
using AutoMapper;
using DataAccess.BLL;
using Web.Api.Models.User;
using Web.Api.Models.Product;
using Web.Api.Models.Invoice;
using Web.Api.Models.Patient;
using Web.Api.Models.Category;

namespace TimeBoxe.Web.Mappings
{
    public class ModelMapping : IAutoMap
    {
        public void Initialize()
        {
            Mapper.CreateMap<UserModel, User>();
            Mapper.CreateMap<User, UserModel>()
          .ForMember(b => b.ConfirmPassword, o => o.MapFrom(m => m.Password));



            Mapper.CreateMap<Product, ProductModel>();
            Mapper.CreateMap<ProductModel, Product>();

            Mapper.CreateMap<Patient, PatientModel>();
            Mapper.CreateMap<PatientModel, Patient>();

            Mapper.CreateMap<Invoice, InvoiceModel>()
            .ForMember(b => b.Date, o => o.Ignore());
            Mapper.CreateMap<InvoiceModel, Invoice>();

            Mapper.CreateMap<InvoiceDetail, ProductGroupModel>();
            Mapper.CreateMap<ProductGroupModel, InvoiceDetail>();


            Mapper.CreateMap<ProductGroup, ProductGroupModel>();
            Mapper.CreateMap<ProductGroupModel, ProductGroup>();


            Mapper.CreateMap<Category, CategoryModel>();
            Mapper.CreateMap<CategoryModel, Category>();



        }
    }
}