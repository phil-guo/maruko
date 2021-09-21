using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.Repos;
using Quartz;

namespace Maruko.TaskScheduling
{
    public abstract class StrategyBase<TJob> : IStrategy
        where TJob : IJob
    {
        private readonly ISchedulerFactory _factory;
        private readonly IFreeSqlRepository<TaskScheduling> _taskSchedule;

        public StrategyBase(ISchedulerFactory factory, IFreeSqlRepository<TaskScheduling> taskSchedule)
        {
            _factory = factory;
            _taskSchedule = taskSchedule;
        }

        protected async Task<IScheduler> GetSchedulerAsync()
        {
            return await _factory.GetScheduler();
        }

        public async Task<AjaxResponse<object>> ExecuteAsync(ExecuteRequest request)
        {
            foreach (var objectId in request.ObjectIds)
            {
                var task = await _taskSchedule.GetAll().Select<TaskScheduling>()
                    .Where(item => item.Id == objectId)
                    .ToOneAsync();

                if (task == null)
                    return new AjaxResponse<object>("任务不存在");

                var scheduler = await GetSchedulerAsync();
                await scheduler.Start();

                var job = JobBuilder.Create<TJob>()
                    .WithIdentity($"job_{objectId}", task.GroupName)
                    .UsingJobData(Map(objectId))
                    .Build();

                //创建一个触发器
                var triggerBuilder = TriggerBuilder.Create()
                    .WithIdentity($"trigger_{objectId}", task.GroupName)
                    .WithCronSchedule(task.CronExpression)
                    .ForJob(job);

                await scheduler.ScheduleJob(job, triggerBuilder.Build());

                await _taskSchedule.GetAll()
                    .Update<TaskScheduling>(objectId)
                    .Set(item => item.StartTime, DateTime.Now)
                    .Set(item => item.Status, true)
                    .ExecuteAffrowsAsync();
            }

            return new AjaxResponse<object>("任务执行成功");
        }

        public async Task<AjaxResponse<object>> CloseAsync(ExecuteRequest request)
        {
            foreach (var objectId in request.ObjectIds)
            {
                var task = await _taskSchedule.GetAll().Select<TaskScheduling>()
                    .Where(item => item.Id == objectId)
                    .ToOneAsync();

                JobKey existKey = JobKey.Create($"Job_{objectId}", task.GroupName);
                TriggerKey existTriggerKey = new TriggerKey($"trigger_{objectId}", task.GroupName);

                var _scheduler = await GetSchedulerAsync();

                await _scheduler.PauseJob(existKey);
                await _scheduler.UnscheduleJob(existTriggerKey);
                await _scheduler.DeleteJob(existKey);

                await _taskSchedule.GetAll()
                    .Update<TaskScheduling>(objectId)
                    .Set(item => item.Status, false)
                    .ExecuteAffrowsAsync();
            }

            return new AjaxResponse<object>("任务关闭成功");
        }

        protected virtual JobDataMap Map(long objectId)
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "objectId", objectId }
            };
            return new JobDataMap(dictionary);
        }
    }
}