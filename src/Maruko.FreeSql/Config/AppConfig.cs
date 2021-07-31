using Autofac;
using Maruko.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Maruko.Core.FreeSql.Config
{
    public class AppConfig
    {
        public FreeSqlOption FreeSql { get; set; }

        private IConfiguration configuration => ServiceLocator.Current.Resolve<IConfiguration>();

        public AppConfig()
        {
            var section = configuration?.GetSection(nameof(FreeSql));
            FreeSql = section.Exists()
                ? section.Get<FreeSqlOption>()
                : new FreeSqlOption();
        }
    }
}
