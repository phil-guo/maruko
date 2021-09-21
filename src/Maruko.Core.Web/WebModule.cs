using Autofac;
using Maruko.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Core.Web
{
    public class WebModule : KernelModule
    {
        public override void ConfigureServices(IServiceCollection service)
        {
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public override void Initialize(ILifetimeScope scope, IApplicationBuilder app)
        {
        }

        protected override void RegisterModule(ContainerBuilder builder)
        {
        }
    }
}