//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：09/02/2021  
//版本1.0
//===================================================================================


using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;

namespace Maruko.TaskScheduling
{
    public class TaskSchedulingService : CurdAppService<TaskScheduling, TaskSchedulingDTO>, ITaskSchedulingService
    {
        public TaskSchedulingService(IObjectMapper objectMapper, IFreeSqlRepository<TaskScheduling> repository) : base(
            objectMapper, repository)
        {
        }
    }
}