using Maruko.Core.FreeSql.Internal.AppService;

namespace Maruko.TaskScheduling
{
    public interface ITaskSchedulingService : ICurdAppService<TaskScheduling, TaskSchedulingDTO>
    {
    }
}