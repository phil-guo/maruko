using System;
using System.IO;
using System.Reflection;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.Modules;
using Maruko.Core.Web.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

namespace Maruko.Core.Web
{
    public class WebModule : KernelModule
    {
        public override void Initialize(ILifetimeScope scope)
        {
            var app = scope.Resolve<IApplicationBuilder>();
            var env = scope.Resolve<IWebHostEnvironment>();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "uploads")),
                RequestPath = "/uploads"
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); });
            app.UseRouting();
            app.UseCors("cors");
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }

        protected override void RegisterModule(ContainerBuilder builder)
        {
            var appConfig = new AppConfig();
            ServiceLocator.ServiceCollection.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(appConfig.Swagger.Version, new OpenApiInfo { Title = appConfig.Swagger.Title, Version = appConfig.Swagger.Version });

                // 获取xml文件名
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // 获取xml文件路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 添加控制器层注释，true表示显示控制器注释
                option.IncludeXmlComments(xmlPath, true);
            });

            ServiceLocator.ServiceCollection.AddCors(option =>
            {
                option.AddPolicy("cors", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });
        }
    }
}
