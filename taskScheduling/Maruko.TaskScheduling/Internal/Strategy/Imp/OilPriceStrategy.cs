using Maruko.Core.FreeSql.Internal.Repos;
using Quartz;

namespace Maruko.TaskScheduling
{
    public class OilPriceStrategy : StrategyBase<OilPriceJob>
    {
        public OilPriceStrategy(ISchedulerFactory factory, IFreeSqlRepository<TaskScheduling> taskSchedule)
            : base(factory, taskSchedule)
        {
        }
    }
}