using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.Extensions.Http;
using Maruko.Core.Quartz.Internal.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Maruko.Core.Quartz.Jobs
{
    [PersistJobDataAfterExecution, DisallowConcurrentExecution]
    public class DefaultBackgroundJob : IJob
    {
        private IMiddleClient _client;
        private ILogger<DefaultBackgroundJob> _logger;
        protected readonly int MaxLogCount = 20; //最多保存日志数量  
        protected Stopwatch Stopwatch = new Stopwatch();

        public async Task Execute(IJobExecutionContext context)
        {
            _logger = ServiceLocator.Current.Resolve<ILogger<DefaultBackgroundJob>>();
            _client = ServiceLocator.Current.Resolve<IMiddleClient>();
            Stopwatch.Restart(); //  开始监视代码运行时间

            //获取相关的参数
            var requestUrl = HttpUtility.UrlDecode(context.JobDetail.JobDataMap.GetString(Constant.REQUESTURL)?.Trim());
            var requestType = context.JobDetail.JobDataMap.GetString(Constant.REQUESTTYPE);
            var scheduleDelay = 2;
            var requestStatus = context.JobDetail.JobDataMap.GetString(Constant.JobRequestStatus);

            System.Enum.TryParse(requestType, out ServiceMethodRequest type);

            var logModel = new LogModel()
            {
                RequestUrl = requestUrl,
                RequestType = requestType
            };

            List<string> logs;

            if (context.JobDetail.JobDataMap[Constant.LOGS] == null)
                logs = new List<string>();
            else
            {
                logs = JsonConvert
                    .DeserializeObject<List<string>>(context.JobDetail.JobDataMap[Constant.LOGS].ToString());
            }

            if (logs.Count >= MaxLogCount)
                logs.RemoveRange(0, logs.Count - MaxLogCount);

            DateTime memoryNextFireTime = default;
            if (!string.IsNullOrEmpty(context.JobDetail.JobDataMap.GetString(Constant.NextRequestTime)))
                memoryNextFireTime = Convert.ToDateTime(context.JobDetail.JobDataMap.GetString(Constant.NextRequestTime));

            try
            {
                if (requestStatus == null ||
                    requestStatus == "True" ||
                    DateTime.Now >= memoryNextFireTime.AddMinutes(Convert.ToInt32(scheduleDelay)))
                {
                    var requestTypeEnum = type;
                    context.JobDetail.JobDataMap[Constant.NextRequestTime] = "";
                    var response = await _client.ExecuteAsync(new RequestModel()
                    {
                        RequestUrl = requestUrl,
                        RequestType = requestTypeEnum
                    });

                    logModel.Body = response.Body;
                    if (!response.Success)
                    {
                        _logger.LogError(response.Msg, response.ErrorMessage);
                        logModel.ErrorMsg = $"业务接口异常，异常信息：{response.Msg}";
                        logModel.IsError = true;
                    }
                    else
                    {
                        context.JobDetail.JobDataMap[Constant.JobRequestStatus] = "True";
                        _logger.LogInformation(JsonConvert.SerializeObject(response));
                        logModel.Result = $"请求地址：{logModel.RequestUrl};请求类型：{requestType.ToUpper()};";
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                logModel.ErrorMsg =
                    $"请求状态：{requestStatus}，下次请求时间：{memoryNextFireTime.AddMinutes(Convert.ToInt32(scheduleDelay))}，当前时间：{DateTime.Now},任务调度异常，异常信息：{exception.Message}";
                logModel.IsError = true;
                context.JobDetail.JobDataMap[Constant.JobRequestStatus] = "False";
                if (string.IsNullOrEmpty(context.JobDetail.JobDataMap.GetString(Constant.NextRequestTime)))
                    context.JobDetail.JobDataMap[Constant.NextRequestTime] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            }
            finally
            {
                Stopwatch.Stop(); //  停止监视
                double seconds = Stopwatch.Elapsed.TotalSeconds; //总秒数           
                logModel.UseTime = seconds;
                logModel.EndTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                logs.Add(JsonConvert.SerializeObject(logModel));
                context.JobDetail.JobDataMap[Constant.LOGS] = JsonConvert.SerializeObject(logs);
            }

            //MarukoQuartzModule.WebUtils.PostAsync<>()
        }
    }
}
