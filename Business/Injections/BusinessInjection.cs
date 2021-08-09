using Business.Concrate;
using Business.Interface;
using DataAccess.Injections;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Injections
{
    public static class BusinessInjection
    {
        public static void Initialize(IServiceCollection services)
        {
            DataAccessInjection.Initialize(services);
            services.AddTransient<IProductService, ProductManager>();
            services.AddTransient<ICategoryService, CategoryManager>();
        }
    }
}
