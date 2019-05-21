using System;
using System.IO;
using System.Linq;
using Maruko.AspNetMvc.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Maruko.AspNetMvc
{
    public static class AspNetMvcConfigureExtension
    {
        public static void UseAspNetMvcConfigure(this IApplicationBuilder app,
            IHostingEnvironment env,
            Action<IApplicationBuilder> action)
        {
            UseAspNetMvcConfigure(app, env, action, true, true);
        }

        public static void UseAspNetMvcConfigure(IApplicationBuilder app, IHostingEnvironment env,
            Action<IApplicationBuilder> action,
            bool isUseStaticFiles = false,
            bool isUseSwagger = false)
        {
            if (isUseStaticFiles)
                UseStaticFiles(app, env);
            if (isUseSwagger)
                UseSwagger(app);

            action(app);
        }

        private static void UseStaticFiles(IApplicationBuilder app, IHostingEnvironment env)
        {
            var path = $"{env.ContentRootPath}/Content";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                RequestPath = "/Content"
            });
        }

        private static void UseSwagger(IApplicationBuilder app)
        {
            const string projectName = "Baby";
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //页面打开时的显示方式:全部折叠(none)、列表(list 默认)、全部打开(full)
                c.DocExpansion(DocExpansion.None);
                //设置一个自定义路由来访问，默认/swagger/ui
                c.RoutePrefix = "document";
                //设置节点说明
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.DocumentTitle = "在线API文档";
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{projectName}.{version}");
                });
            });
        }
    }
}
