using DataAccess.Concrate;
using DataAccess.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Injections
{
    public static class DataAccessInjection
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddTransient<IProductDal, ProductDal>();
            services.AddTransient<ICategoryDal, CategoryDal>();
        }
    }
}
