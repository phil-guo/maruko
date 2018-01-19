using log4net;
using Maruko.Logger;
using SqlSugar;

namespace Maruko.SqlSugarCore
{
    public abstract class InstallSqlSugar
    {
        public virtual string ConnectionString { get; set; }

        public ILog Logger { get; }

        protected InstallSqlSugar()
        {
            Logger = LogHelper.Log4NetInstance.LogFactory(typeof(InstallSqlSugar));
        }

        public SqlSugarClient Instence(bool isEnableLog = false, InitKeyType type = InitKeyType.SystemTable)
        {
            var sugarClient = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = type
            });

            if (isEnableLog)
                WriteSqlLog(sugarClient);

            return sugarClient;
        }

        private void WriteSqlLog(SqlSugarClient sugarClient)
        {
            sugarClient.Ado.IsEnableLogEvent = true;
            sugarClient.Ado.LogEventStarting = (sql, pars) =>
            {
                Logger.Info($"sql:{sql}，params:{pars} ");
            };
        }
    }
}
