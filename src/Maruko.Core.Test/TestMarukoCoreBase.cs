using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Maruko.Core.Extensions;
using Maruko.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Xunit;
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

            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("config/test.json", true, true)
            //    .AddEnvironmentVariables();
            //var configuration = builder.Build();
            //ServiceLocator.Configuration = configuration;
            //var serviceCollection = new ServiceCollection();
            //var containerBuilder = new ContainerBuilder();
            //serviceCollection.AddSingleton<IConfiguration>(configuration);
            //AddService(serviceCollection);
            //containerBuilder.Populate(serviceCollection);
            //ServiceLocator.ServiceCollection = serviceCollection;

            //RegisterModule(containerBuilder);
            //containerBuilder.Build();
        }

        //protected virtual void AddService(ServiceCollection service)
        //{
        //    service.AddSingleton<IApplicationBuilder, ApplicationBuilder>();
        //    service.AddOptions();
        //}


        //protected virtual void RegisterModule(ContainerBuilder builder)
        //{
        //}
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