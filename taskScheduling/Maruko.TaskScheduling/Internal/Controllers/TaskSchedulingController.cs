using System.Threading.Tasks;
using Autofac;
using Maruko.Core.Application;
using Maruko.Core.Extensions;
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
        private readonly IStrategy _strategy;

        public TaskSchedulingController(ICurdAppService<TaskScheduling, TaskSchedulingDTO> curd) : base(curd)
        {
            _strategy = ServiceLocator.Current.ResolveKeyed<IStrategy>(Strategy.Oil);
        }

        [HttpPost("executeAsync")]
        public async Task<AjaxResponse<object>> ExecuteAsync(ExecuteRequest request)
        {
            return await _strategy.ExecuteAsync(request);
        }
    }
}