using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Maruko.Core.Extensions;
using Maruko.Core.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config/test.json", true, true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            var containerBuilder = new ContainerBuilder();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            AddService(serviceCollection);
            containerBuilder.Populate(serviceCollection);
            containerBuilder.RegisterModules(configuration);
            RegisterModule(containerBuilder);
            containerBuilder.Build();
        }

        protected virtual void AddService(ServiceCollection service)
        {

        }


        protected virtual void RegisterModule(ContainerBuilder builder)
        {

        }
    }
}