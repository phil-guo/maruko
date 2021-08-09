using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Maruko.Dynamic.Config
{
    public class AppConfig
    {
        public static AppConfig Default = new AppConfig();

        public DynamicConfigOption DynamicConfig { get; set; }

        private IConfiguration configuration => ServiceLocator.Current.Resolve<IConfiguration>();

        public AppConfig()
        {
            var section = configuration?.GetSection(nameof(DynamicConfig));
            DynamicConfig = section.Exists()
                ? section.Get<DynamicConfigOption>()
                : new DynamicConfigOption();
        }
    }
}
