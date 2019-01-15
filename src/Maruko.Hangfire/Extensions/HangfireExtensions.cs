using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Hangfire.Extensions
{
    public static class HangfireExtensions
    {
        public static void AddHangfire(this IServiceCollection services)
        {
            services.AddHangfire(x => x.UseMemoryStorage());
        }

        public static void UseHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] { new CustomAuthorizeFilter(), }
            });
        }
    }
}
