using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Configuration;
using Maruko.MongoDB.MongoDBRepos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.MongoDB.Extensions
{
    public static class MongoDbExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            services.Configure<MongodbSettings>(options => ConfigurationSetting.DefaultConfiguration.GetSection(nameof(MongodbSettings)).Bind(options));

            services.AddScoped<MongoDbContext>();
            services.AddScoped(typeof(MongoDbBaseRepository<,>));

            return services;
        }
    }
}
