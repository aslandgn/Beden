using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SzDataAccess.Concrate;
using SzDataAccess.Interface;

namespace SzDataAccess.Injections
{
    public static class SzDataAccessInjections
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SzContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddTransient<ISizeDal, SizeDal>();
            services.AddTransient<ISizeTypeDal, SizeTypeDal>();
        }
    }
}
