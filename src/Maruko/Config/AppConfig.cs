using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Maruko.Core.Config
{
    public class AppConfig
    {
        public CoreOption Core { get; set; }

        public AppConfig(IConfiguration configuration)
        {
            var section = configuration?.GetSection(nameof(Core));
            Core = section.Exists()
                ? section.Get<CoreOption>()
                : new CoreOption();
        }
    }
}
