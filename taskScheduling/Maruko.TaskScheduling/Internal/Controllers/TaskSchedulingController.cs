using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.TaskScheduling
{
    [EnableCors("cors")]
    [Route("api/v1/taskschedulings/")]
    public class TaskSchedulingController : BaseCurdController<TaskScheduling, TaskSchedulingDTO>
    {
        public TaskSchedulingController(ICurdAppService<TaskScheduling, TaskSchedulingDTO> curd) : base(curd)
        {
        }
    }
}