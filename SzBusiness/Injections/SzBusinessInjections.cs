using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SzBusiness.Concrate;
using SzBusiness.Interface;
using SzDataAccess.Injections;

namespace SzBusiness.Injections
{
    public static class SzBusinessInjections
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            SzDataAccessInjections.Initialize(services, configuration);
            services.AddTransient<ISizeService, SizeManager>();
            services.AddTransient<ISizeTypeService, SizeTypeManager>();
        }
    }
}
