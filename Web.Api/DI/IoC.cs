using Service.Contracts;
using Service.Services;
using StructureMap;
using StructureMap.Graph;
using System.Web.ApplicationServices;

namespace Web.Api.DI
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.WithDefaultConventions();
                    scan.TheCallingAssembly();
                    scan.Assembly("DataAccess");
                    scan.Assembly("Service");

                });

                x.For<IAppService>().Use<AppService>();
                x.For<ISpService>().Use<SpService>();
                x.For<IRoleService>().Use<Service.Services.RoleService>();
                x.For<IUserService>().Use<UserService>();
                x.For<IPermissionService>().Use<PermissionService>();
                x.For<IRolePermissionService>().Use<RolePermissionService>();
                x.For<IUserLoginService>().Use<UserLoginService>();
                x.For<IReportService>().Use<ReportService>();
                x.For<IReportCatalogueService>().Use<ReportCatalogueService>();
                x.For<IPatientService>().Use<PatientService>();
                x.For<ISystemSettingService>().Use<SystemSettingService>();
                x.For<IActionCategoryService>().Use<ActionCategoryService>();
                x.For<IActionService>().Use<ActionService>();
                x.For<ICountryService>().Use<CountryService>();
                x.For<ICompanyService>().Use<CompanyService>();
                x.For<IProductService>().Use<ProductService>();
                x.For<IInvoiceDetailService>().Use<InvoiceDetailService>();
                x.For<IProductGroupService>().Use<ProductGroupService>();
                x.For<IProductMapService>().Use<ProductMapService>();
                x.For<ICategoryService>().Use<CategoryService>();

            });
            return ObjectFactory.Container;
        }
    }
}


