using FreeSql;
using Maruko.Core.FreeSql.Config;

namespace Maruko.Core.FreeSql.Internal.Context
{
    public class FreeSqlContext : IFreeSqlContext
    {
        private readonly IFreeSql _freeSql;

        public FreeSqlContext()
        {
            var appConfig = new AppConfig();

            _freeSql = new FreeSqlBuilder()
                .UseConnectionString(appConfig.FreeSql.DbType, appConfig.FreeSql.Connection)
                .Build();
        }

        public IFreeSql GetSet()
        {
            return _freeSql;
        }
    }
}
