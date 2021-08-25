using Microsoft.Extensions.DependencyInjection;
using PrdBusiness.Concrate;
using PrdBusiness.Interface;
using PrdDataAccess.Injections;

namespace PrdBusiness.Injections
{
    public static class PrdBusinessInjection
    {
        public static void Initialize(IServiceCollection services)
        {
            PrdDataAccessInjection.Initialize(services);
            services.AddTransient<IProductService, ProductManager>();
            services.AddTransient<ICategoryService, CategoryManager>();
        }
    }
}
