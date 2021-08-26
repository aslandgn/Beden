using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrdDataAccess.Concrate;
using PrdDataAccess.Interface;

namespace PrdDataAccess.Injections
{
    public static class PrdDataAccessInjection
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PrdContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddTransient<IProductDal, ProductDal>();
            services.AddTransient<ICategoryDal, CategoryDal>();
        }
    }
}
