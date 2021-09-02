using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Maruko.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Maruko.Core.Test
{
    public abstract class TestMarukoCoreBase
    {
        private readonly ITestOutputHelper _outputHelper;

        public TestMarukoCoreBase(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;

            Initialize();
        }

        protected void Print(object obj)
        {
            _outputHelper.WriteLine(JsonConvert.SerializeObject(obj));
        }

        protected void Initialize()
        {
            Host.CreateDefaultBuilder(new string[] { })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile($"config/test.json", true,
                        true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).Build();
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            ServiceLocator.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ServiceLocator.ServiceCollection = services;
            services.AddSingleton<IApplicationBuilder, ApplicationBuilder>();
            services.AddAuthorization();
            services.ConfigureMarukoServices(ServiceLocator.Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModules(ServiceLocator.Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMaruko();
        }
    }
}