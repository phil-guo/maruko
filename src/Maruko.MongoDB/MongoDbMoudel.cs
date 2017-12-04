using Maruko.Modules;
using Microsoft.Extensions.DependencyInjection;
using Maruko.MongoDB.MongoDBRepos;
using Maruko.Configuration;
using Microsoft.Extensions.Configuration;

namespace Maruko.MongoDB
{
    [LoadOn(false, "Maruko.MongoDB")]
    public static class MongoDbMoudel
    {
        public static void AddMongoDb(this IServiceCollection services)
        {
            services.Configure<MongodbSettings>(options => ConfigurationSetting.DefaultConfiguration.GetSection(nameof(MongodbSettings)).Bind(options));

            services.AddScoped<MongoDbContext>();
            services.AddScoped(typeof(MongoDbBaseRepository<,>));
        }
    }
}
