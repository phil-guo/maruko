using Autofac;
using Maruko.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Maruko.Core.FreeSql.Config
{
    public class AppConfig
    {
        public FreeSqlOption FreeSql { get; set; }

        public AppConfig(IConfiguration configuration)
        {
            var section = configuration?.GetSection(nameof(FreeSql));
            FreeSql = section.Exists()
                ? section.Get<FreeSqlOption>()
                : new FreeSqlOption();
        }
    }
}
