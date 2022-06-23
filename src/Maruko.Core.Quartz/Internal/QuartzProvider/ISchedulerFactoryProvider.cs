using System.Threading.Tasks;
using Quartz;

namespace Maruko.Core.Quartz.Internal.QuartzProvider
{
    public interface ISchedulerFactoryProvider
    {
        Task<IScheduler> GetScheduler();
    }
}
