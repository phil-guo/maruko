﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Async;
using Maruko.Core.Extensions.Http;
using Maruko.Core.Modules;
using Maruko.Core.Quartz.Internal.QuartzProvider;
using Microsoft.AspNetCore.Builder;

namespace Maruko.Core.Quartz
{
    public class MarukoQuartzModule :  KernelModule
    {
        public static WebUtilsHttpConnectionPool WebUtils;

        public override void Initialize(ILifetimeScope scope, IApplicationBuilder application)
        {
            WebUtils = new WebUtilsHttpConnectionPool("http://127.0.0.1", 50);
            AsyncHelper.RunSync(async () => await scope.Resolve<IScheduleProvider>().StartAsync());
        }

        protected override void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterType<SchedulerFactoryProvider>().As<ISchedulerFactoryProvider>().SingleInstance();
            builder.RegisterType<QuartzDbProvider>().As<IQuartzDbProvider>();
            builder.RegisterType<ScheduleProvider>().As<IScheduleProvider>();
        }
    }
}
