using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Maruko.Core.Application;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.Extensions.Linq;
using Maruko.Core.Quartz.Internal.Models;
using Maruko.Core.Quartz.Internal.QuartzProvider;
using Maruko.Core.Quartz.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;

namespace Maruko.Core.Quartz.Controllers
{
    [Route("api/v1/quartzs")]
    public class QuartzController : ControllerBase
    {
        private readonly IScheduleProvider _scheduleProvider;
        private readonly ISchedulerFactoryProvider _schedulerFactoryProvider;

        public QuartzController(IScheduleProvider scheduleProvider, ISchedulerFactoryProvider schedulerFactoryProvider)
        {
            _scheduleProvider = scheduleProvider;
            _schedulerFactoryProvider = schedulerFactoryProvider;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("pageSearch")]
        public async Task<AjaxResponse<PagedResultDto>> GetPageData([FromBody]PageDto model)
        {
            var jobs = (await _scheduleProvider.GetAllJobAsync())
                .OrderBy(item => item.AppId)
                .ToList();

            var data = jobs
                .AsQueryable()
                .Where(QueryableExtensions.ConditionToLambda<JobInfoModel>(model))
                .Skip((model.PageIndex - 1) * model.PageMax)
                .Take(model.PageMax)
                .ToList();

            var total = jobs
                .AsQueryable()
                .Where(QueryableExtensions.ConditionToLambda<JobInfoModel>(model))
                .Count();

            return new AjaxResponse<PagedResultDto>(new PagedResultDto(total, data), "查询成功");
        }

        /// <summary>
        /// 添加一个job
        /// </summary>
        /// <returns></returns>
        [HttpPost("createAsync")]
        public async Task<AjaxResponse<object>> AddScheduleJobAsync([FromBody]ScheduleModel model, long? runNumber = null)
        {
            var scheduler = await _schedulerFactoryProvider.GetScheduler();

            //#if DEBUG
            //            await AddScheduleJobAsync_Debug<DefaultBackgroundJob>(model, runNumber);
            //#endif

            await _scheduleProvider.AddScheduleJobAsync<DefaultBackgroundJob>(model, runNumber);
            await scheduler.PauseJob(new JobKey(model.JobName, model.JobGroup));
            return new AjaxResponse<object>("添加成功");
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        [HttpPost("triggerExecuteAsync")]
        public async Task<AjaxResponse<object>> TriggerExecuteAsync([FromBody]JobKey jobKey)
        {
            var schedule = await _schedulerFactoryProvider.GetScheduler();
            await schedule.ResumeJob(jobKey);

            return new AjaxResponse<object>("执行成功");
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        [HttpPost("pauseJobAsync")]
        public async Task<AjaxResponse<object>> PauseJobAsync([FromBody] JobKey jobKey)
        {
            var schedule = await _schedulerFactoryProvider.GetScheduler();
            await schedule.PauseJob(jobKey);

            return new AjaxResponse<object>("执行成功");
        }

        /// <summary>
        /// 删除任务
        /// </summary> 
        /// <returns></returns>
        [HttpPost, Route("removeAsync")]
        public async Task<AjaxResponse<object>> RemoveJob([FromBody] JobKey job)
        {
            await _scheduleProvider.StopOrDelScheduleJobAsync(job.Group, job.Name, true);
            return new AjaxResponse<object>("执行成功");
        }

        /// <summary>
        /// 查询任务
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("getDetailJobAsync")]
        public async Task<AjaxResponse<object>> QueryJob(string group, string name)
        {
            //#if DEBUG
            //            var resultDebug = await QueryJobAsync_Debug(group, name);
            //#endif

            var result = await _scheduleProvider.QueryJobAsync(group, name);
            return new AjaxResponse<object>(result, "执行成功");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("editAsync")]
        public async Task<AjaxResponse<object>> UpdateJob([FromBody] ScheduleModel request)
        {
            var result = await _scheduleProvider.QueryJobAsync(request.JobGroup, request.JobName);

            var updateModel = new UpdateJobModel()
            {
                NewModel = request,
                OldModel = result
            };

            if (updateModel.NewModel.TriggerType == TriggerTypeEnum.Cron && updateModel.NewModel.Cron == "* * * * * ?")
            {
                return new AjaxResponse<object>("当前环境不允许过频繁执行任务", 403);
            }

            await _scheduleProvider.UpdateJob<DefaultBackgroundJob>(updateModel);

            return new AjaxResponse<object>("执行成功");
        }

        /// <summary>
        /// 获取job日志
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        [HttpPost, Route("getJobLogsAsync")]
        [AllowAnonymous]
        public async Task<AjaxResponse<object>> GetJobLogs([FromBody] JobKey jobKey)
        {
            //#if DEBUG
            //            var logs_debug = await GetJobLogsAsync_Debug(jobKey);
            //#endif

            var logs = await _scheduleProvider.GetJobLogsAsync(jobKey);
            logs?.Reverse();
            return new AjaxResponse<object>(logs, "执行成功");
        }
    }
}
