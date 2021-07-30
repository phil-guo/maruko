using FreeSql;
using Maruko.FreeSql.Config;

namespace Maruko.FreeSql.Internal.Context
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
