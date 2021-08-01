using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Maruko.Zero.Config
{
    public class AppConfig
    {
        public ZeroOption Zero { get; set; }

        private IConfiguration configuration => ServiceLocator.Current.Resolve<IConfiguration>();

        public AppConfig()
        {
            var section = configuration?.GetSection(nameof(Zero));
            Zero = section.Exists()
                ? section.Get<ZeroOption>()
                : new ZeroOption();
        }
    }
}
