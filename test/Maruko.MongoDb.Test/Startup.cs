using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Maruko.MongoDB;
using Maruko.Configuration;

namespace Maruko.MongoDb.Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            ConfigurationSetting.DefaultConfiguration = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddSingleton(factory => services);
            //services.AddSingleton(factory => Configuration);
            //services.AddOptions();

            services.AddMongoDb();
            //services.Configure<MongodbSettings>(options => Configuration.GetSection(nameof(MongodbSettings)).Bind(options));

            //services.AddSingleton<MongoDbContext>();
            //services.AddScoped(typeof(MongoDbBaseRepository<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
