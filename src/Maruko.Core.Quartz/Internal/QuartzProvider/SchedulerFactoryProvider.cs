using System.Threading.Tasks;
using Quartz;
using Quartz.Impl.AdoJobStore;

namespace Maruko.Core.Quartz.Internal.QuartzProvider
{
    public class SchedulerFactoryProvider : ISchedulerFactoryProvider
    {
        private IScheduler _scheduler;
        private readonly IQuartzDbProvider _quartzDbProvider;
        public SchedulerFactoryProvider(IQuartzDbProvider quartzDbProvider)
        {
            _quartzDbProvider = quartzDbProvider;
        }

        public async Task<IScheduler> GetScheduler()
        {
            return _scheduler ??= await Builder().GetScheduler();
        }

        private ISchedulerFactory Builder()
        {

            var config = SchedulerBuilder.Create();
            config.UsePersistentStore(store =>
            {
                store.UseProperties = true;

                store.UseMySql(dbOption =>
                {
                    dbOption.ConnectionString = _quartzDbProvider.Connection;
                    dbOption.TablePrefix = "qz_";
                    dbOption.UseDriverDelegate<MySQLDelegate>();
                });
                store.UseJsonSerializer();
            });
            config.SchedulerId = "AUTO";
            return config.Build();
        }
    }
}
