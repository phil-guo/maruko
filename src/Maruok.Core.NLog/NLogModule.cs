using System;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using NLog;
using NLog.Extensions.Logging;

namespace Maruko.Core.NLog
{
    public class NLogModule : KernelModule
    {
        public override void Initialize(ILifetimeScope scope, IApplicationBuilder app)
        {
            LogManager.LoadConfiguration("nlog.config");
        }

        public override void ConfigureServices(IServiceCollection service)
        {
            service.AddLogging(logger =>
            {
                logger.ClearProviders();
                logger.AddNLog();
            });
        }
    }
}
