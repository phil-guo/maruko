using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maruko.Core.Quartz.Internal.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using Constant = Maruko.Core.Quartz.Internal.Models.Constant;

namespace Maruko.Core.Quartz.Internal.QuartzProvider
{
    public class ScheduleProvider : IScheduleProvider
    {
        private readonly ISchedulerFactoryProvider _provider;
        private readonly ILogger<ScheduleProvider> _logger;

        public ScheduleProvider(ISchedulerFactoryProvider provider, ILogger<ScheduleProvider> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        public async Task StartAsync()
        {
            var schedule = await _provider.GetScheduler();
            if (schedule.InStandbyMode) //是否是待机模式
                await schedule.Start();

            if (!schedule.InStandbyMode)
                _logger.LogInformation("已开启任务调度...");
        }

        public async Task AddScheduleJobAsync<TJob>(ScheduleModel entity, long? runNumber = null)
               where TJob : IJob
        {
            //检查任务是否已存在
            var scheduler = await _provider.GetScheduler();
            var jobKey = new JobKey(entity.JobName, entity.JobGroup);
            if (await scheduler.CheckExists(jobKey))
                throw new Exception($"任务{entity.JobGroup}.{entity.JobName}已经存在...");
            //http请求配置
            var httpDir = new Dictionary<string, string>()
                {
                    //{ Constant.EndAt, entity.EndTime.ToString()},
                    { Constant.JobTypeEnum, ((int)entity.JobType).ToString()},
                    { Constant.MAILMESSAGE, ((int)entity.MailMessage).ToString()},
                    { Constant.DATAHANDLER,entity.DataHandler }
                };
            if (runNumber.HasValue)
                httpDir.Add(Constant.RUNNUMBER, runNumber.ToString());

            IJobConfigurator jobConfigurator = null;
            if (entity.JobType == JobTypeEnum.Url)
            {
                jobConfigurator = JobBuilder.Create<TJob>();
                httpDir.Add(Constant.REQUESTURL, entity.RequestUrl);
                httpDir.Add(Constant.HEADERS, entity.RequestHeader);
                httpDir.Add(Constant.REQUESTPARAMETERS, entity.RequestParameters);
                httpDir.Add(Constant.REQUESTTYPE, entity.RequestType.ToString());
                httpDir.Add(Constant.AppId, entity.AppId);
                httpDir.Add(Constant.JobRequestStatus, "True");
            }

            // 定义这个工作，并将其绑定到我们的IJob实现类                
            IJobDetail job = jobConfigurator
                .SetJobData(new JobDataMap(httpDir))
                .WithDescription(entity.Description)
                .WithIdentity(entity.JobName, entity.JobGroup)
                .Build();
            // 创建触发器
            ITrigger trigger = null;
            //校验是否正确的执行周期表达式
            if (entity.TriggerType == TriggerTypeEnum.Cron)//
            {
                if (!CronExpression.IsValidExpression(entity.Cron))
                    throw new Exception("Cron表达式不正确...");

                trigger = CreateCronTrigger(entity);
            }

            // 告诉Quartz使用我们的触发器来安排作业
            await scheduler.ScheduleJob(job, trigger);
        }

        public async Task UpdateJob<TJob>(UpdateJobModel request)
            where TJob : IJob
        {
            var jobKey = new JobKey(request.OldModel.JobName, request.OldModel.JobGroup);
            var runNumber = await GetRunNumberAsync(jobKey);
            await StopOrDelScheduleJobAsync(request.OldModel.JobGroup, request.OldModel.JobName, true);
            await AddScheduleJobAsync<TJob>(request.NewModel, runNumber);
        }

        public async Task StopOrDelScheduleJobAsync(string jobGroup, string jobName, bool isDelete = false)
        {
            var scheduler = await _provider.GetScheduler();
            await scheduler.PauseJob(new JobKey(jobName, jobGroup));
            if (isDelete)
                await scheduler.DeleteJob(new JobKey(jobName, jobGroup));
        }

        public async Task ResumeJobAsync(string jobGroup, string jobName)
        {
            var scheduler = await _provider.GetScheduler();
            var jobKey = new JobKey(jobName, jobGroup);
            if (!await scheduler.CheckExists(jobKey))
                throw new Exception("任务不存在...");

            //var jobDetail = await scheduler.GetJobDetail(jobKey);
            //var endTime = jobDetail?.JobDataMap.GetString("EndAt");
            //if (!string.IsNullOrWhiteSpace(endTime) && DateTime.Parse(endTime) <= DateTime.Now)
            //    throw new InfoException("Job的结束时间已过期");

            //任务已经存在则暂停任务
            await scheduler.ResumeJob(jobKey);
        }

        public async Task<ScheduleModel> QueryJobAsync(string jobGroup, string jobName)
        {
            var model = new ScheduleModel();
            var scheduler = await _provider.GetScheduler();
            var jobKey = new JobKey(jobName, jobGroup);
            var jobDetail = await scheduler.GetJobDetail(jobKey);
            var triggersList = await scheduler.GetTriggersOfJob(jobKey);
            var triggers = triggersList.AsEnumerable().FirstOrDefault();
            //var intervalSeconds = (triggers as SimpleTriggerImpl)?.RepeatInterval.TotalSeconds;
            //var endTime = jobDetail?.JobDataMap.GetString("EndAt");
            //model.BeginTime = triggers.StartTimeUtc.LocalDateTime;

            //if (!string.IsNullOrWhiteSpace(endTime))
            //    model.EndTime = DateTime.Parse(endTime);
            //if (intervalSeconds.HasValue)
            //    model.IntervalSecond = Convert.ToInt32(intervalSeconds.Value);

            model.JobName = jobName;
            model.JobGroup = jobGroup;
            model.Cron = (triggers as CronTriggerImpl)?.CronExpressionString;
            //model.RunTimes = (triggers as SimpleTriggerImpl)?.RepeatCount;
            //model.TriggerType = triggers is SimpleTriggerImpl ? TriggerTypeEnum.Simple : TriggerTypeEnum.Cron;
            model.MailMessage = (MailMessageEnum)int.Parse(jobDetail?.JobDataMap.GetString(Constant.MAILMESSAGE) ?? "0");
            model.Description = jobDetail?.Description;
            model.AppId = jobDetail?.JobDataMap.GetString(Constant.AppId);
            model.JobType = (JobTypeEnum)int.Parse(jobDetail?.JobDataMap.GetString(Constant.JobTypeEnum) ?? "1");
            model.DataHandler = jobDetail?.JobDataMap.GetString(Constant.DATAHANDLER);
            switch (model.JobType)
            {
                case JobTypeEnum.None:
                    break;
                case JobTypeEnum.Url:
                    model.RequestUrl = jobDetail?.JobDataMap.GetString(Constant.REQUESTURL);
                    model.RequestType = (RequestTypeEnum)System.Enum.Parse(typeof(RequestTypeEnum), jobDetail?.JobDataMap.GetString(Constant.REQUESTTYPE) ?? "", true);
                    model.RequestParameters = jobDetail?.JobDataMap.GetString(Constant.REQUESTPARAMETERS);
                    model.RequestHeader = jobDetail?.JobDataMap.GetString(Constant.HEADERS);
                    break;
                default:
                    break;
            }
            return model;
        }

        public async Task TriggerJobAsync(JobKey jobKey)
        {
            var schedule = await _provider.GetScheduler();
            await schedule.TriggerJob(jobKey);

        }

        public async Task<List<string>> GetJobLogsAsync(JobKey jobKey)
        {
            var schedule = await _provider.GetScheduler();
            var jobDetail = await schedule.GetJobDetail(jobKey);
            if (jobDetail == null)
                throw new Exception("组名、任务名称不正确！");
            List<string> logs;

            if (jobDetail.JobDataMap[Constant.LOGS] == null)
                logs = new List<string>();
            else
            {
                logs = JsonConvert
                    .DeserializeObject<List<string>>(jobDetail.JobDataMap[Constant.LOGS].ToString());
            }
            return logs;
        }

        public async Task<long> GetRunNumberAsync(JobKey jobKey)
        {
            var schedule = await _provider.GetScheduler();
            var jobDetail = await schedule.GetJobDetail(jobKey);
            return jobDetail?.JobDataMap.GetLong(Constant.RUNNUMBER) ?? 0;
        }

        public async Task<List<JobInfoModel>> GetAllJobAsync()
        {
            var jobKeys = new List<JobKey>();
            var jobs = new List<JobInfoModel>();
            var scheduler = await _provider.GetScheduler();
            var groupNames = await scheduler.GetJobGroupNames();
            foreach (var groupName in groupNames)
            {
                jobKeys.AddRange(await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName)));
            }

            foreach (var jobKey in jobKeys.OrderBy(t => t.Name))
            {
                var jobDetail = await scheduler.GetJobDetail(jobKey);
                var triggersList = await scheduler.GetTriggersOfJob(jobKey);
                var triggers = triggersList.AsEnumerable().FirstOrDefault();

                if (triggers == null)
                    continue;
                var interval = (triggers as CronTriggerImpl)?.CronExpressionString;

                //旧代码没有保存JobTypeEnum，所以None可以默认为Url。
                var jobType = (JobTypeEnum)jobDetail.JobDataMap.GetLong(Constant.JobTypeEnum);
                jobType = jobType == JobTypeEnum.None ? JobTypeEnum.Url : jobType;

                var triggerAddress = string.Empty;
                if (jobType == JobTypeEnum.Url)
                    triggerAddress = jobDetail.JobDataMap.GetString(Constant.REQUESTURL);

                //Constant.MailTo
                jobs.Add(new JobInfoModel()
                {
                    GroupName = jobKey.Group,
                    Name = jobKey.Name,
                    LastErrMsg = jobDetail.JobDataMap.GetString(Constant.EXCEPTION),
                    TriggerAddress = triggerAddress,
                    DataHandler = jobDetail.JobDataMap.GetString(Constant.DATAHANDLER),
                    TriggerState = await scheduler.GetTriggerState(triggers?.Key),
                    PreviousFireTime = triggers?.GetPreviousFireTimeUtc()?.LocalDateTime,
                    NextFireTime = triggers?.GetNextFireTimeUtc()?.LocalDateTime,
                    //BeginTime = triggers.StartTimeUtc.LocalDateTime,
                    Interval = interval,
                    //EndTime = triggers.EndTimeUtc?.LocalDateTime,
                    Description = jobDetail.Description,
                    RequestType = jobDetail.JobDataMap.GetString(Constant.REQUESTTYPE),
                    RunNumber = jobDetail.JobDataMap.GetLong(Constant.RUNNUMBER),
                    JobType = (long)jobType,
                    RequestHeader = jobDetail.JobDataMap.GetString(Constant.HEADERS),
                    AppId = jobDetail.JobDataMap.GetString(Constant.AppId),
                });
            }

            return jobs;
        }

        public async Task StopScheduleAsync()
        {
            var schedule = await _provider.GetScheduler();
            if (schedule.InStandbyMode)
                await schedule.Standby();//暂停

            if (schedule.InStandbyMode)
                _logger.LogInformation("任务暂停成功...");
        }


        /// <summary>
        /// 创建类型Cron的触发器
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ITrigger CreateCronTrigger(ScheduleModel entity)
        {
            // 作业触发器
            return TriggerBuilder.Create()
                .WithIdentity(entity.JobName, entity.JobGroup)
                .WithCronSchedule(entity.Cron,
                    cronScheduleBuilder => cronScheduleBuilder
                    .WithMisfireHandlingInstructionFireAndProceed())//指定cron表达式
                .ForJob(entity.JobName, entity.JobGroup)//作业名称
                .Build();
        }
    }
}
