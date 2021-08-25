using Microsoft.Extensions.DependencyInjection;
using PrdDataAccess.Concrate;
using PrdDataAccess.Interface;

namespace PrdDataAccess.Injections
{
    public static class PrdDataAccessInjection
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddTransient<IProductDal, ProductDal>();
            services.AddTransient<ICategoryDal, CategoryDal>();
        }
    }
}
