using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Config;
using Maruko.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Maruko.FreeSql.Config
{
    public class AppConfig
    {
        public FreeSqlOption FreeSql { get; set; }

        private IConfiguration configuration => AutofacExtensions.Current.Resolve<IConfiguration>();

        public AppConfig()
        {
            var section = configuration?.GetSection(nameof(FreeSql));
            FreeSql = section.Exists()
                ? section.Get<FreeSqlOption>()
                : new FreeSqlOption();
        }
    }
}
