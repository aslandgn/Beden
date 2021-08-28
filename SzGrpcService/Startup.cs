using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ServiceExtensions;
using System.IO;
using SzBusiness.Injections;
using SzGrpcService.Mappings;

namespace SzGrpcService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            //_register = register;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        //public IRegister _register { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //logging
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            //browsing protobufs on browser
            services.AddDirectoryBrowser();

            //swagger
            services.AddGrpcHttpApi();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "gRPC HTTP Sz API", Version = "v1" });
            });
            services.AddGrpcSwagger();

            //automapper
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            SzMapper.Register(typeAdapterConfig);
            var mapperConfig = new Mapper(typeAdapterConfig);

            // dependencyInjection
            services.AddSingleton<IMapper>(mapperConfig);
            SzBusinessInjections.Initialize(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "gRPC HTTP Sz API V1");
            });

            app.UseMiddleware<LogMiddleware>();

            //browsing protobufs on browser
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings.Clear();
            provider.Mappings[".proto"] = "text/plain";
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Protos")),
                RequestPath = "/protos",
                ContentTypeProvider = provider

            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Protos")),
                RequestPath = "/protos"
            });

            app.UseEndpoints(endpoints =>
            {
                //grpcServices
                endpoints.MapGrpcService<SizeService>();
                endpoints.MapGrpcService<SizeTypeService>();

                endpoints.MapGet("/", async context =>
                {
                    context.Response.Redirect("/swagger");
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
