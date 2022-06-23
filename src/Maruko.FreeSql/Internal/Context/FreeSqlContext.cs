using Autofac;
using FreeSql;
using FreeSql.Internal;
using Maruko.Core.Domain.Entities;
using Maruko.Core.Extensions;
using Maruko.Core.FreeSql.Config;
using Microsoft.Extensions.Configuration;

namespace Maruko.Core.FreeSql.Internal.Context
{
    public class FreeSqlContext : IFreeSqlContext
    {
        private readonly IFreeSql _freeSql;

        private IConfiguration configuration => ServiceLocator.Current.Resolve<IConfiguration>();

        public FreeSqlContext()
        {
            var appConfig = new AppConfig(configuration);
            var coreAppConfig = new Core.Config.AppConfig(configuration);

            _freeSql = new FreeSqlBuilder()
                .UseConnectionString(appConfig.FreeSql.DbType, coreAppConfig.Core.Connection)
                .Build();

            _freeSql.GlobalFilter.ApplyOnly<ISoftDelete>("IsDelete", item => !item.IsDelete);
        }

        public IFreeSql GetSet()
        {
            return _freeSql;
        }
    }
}
