using System.Collections.Generic;
using System.Threading.Tasks;
using Maruko.Core.Quartz.Internal.Models;
using Quartz;

namespace Maruko.Core.Quartz.Internal.QuartzProvider
{
    public interface IScheduleProvider
    {
        /// <summary>
        /// 开启调度器
        /// </summary>
        /// <returns></returns>
        Task StartAsync();

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <returns></returns>
        Task AddScheduleJobAsync<TJob>(ScheduleModel entity, long? runNumber = null)
               where TJob : IJob;

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task UpdateJob<TJob>(UpdateJobModel request) where TJob : IJob;

        /// <summary>
        /// 暂停/删除 指定的计划
        /// </summary>
        /// <param name="jobGroup">任务分组</param>
        /// <param name="jobName">任务名称</param>
        /// <param name="isDelete">停止并删除任务</param>
        /// <returns></returns>
        Task StopOrDelScheduleJobAsync(string jobGroup, string jobName, bool isDelete = false);

        /// <summary>
        /// 恢复运行暂停的任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="jobGroup">任务分组</param>
        Task ResumeJobAsync(string jobGroup, string jobName);

        /// <summary>
        /// 查询任务
        /// </summary>
        /// <param name="jobGroup"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        Task<ScheduleModel> QueryJobAsync(string jobGroup, string jobName);

        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        Task TriggerJobAsync(JobKey jobKey);

        /// <summary>
        /// 获取job日志
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        Task<List<string>> GetJobLogsAsync(JobKey jobKey);

        /// <summary>
        /// 获取运行次数
        /// </summary>
        /// <returns></returns>
        Task<long> GetRunNumberAsync(JobKey jobKey);

        /// <summary>
        /// 获取所有的job
        /// </summary>
        /// <returns></returns>
        Task<List<JobInfoModel>> GetAllJobAsync();


        /// <summary>
        /// 暂停任务调度
        /// </summary>
        /// <returns></returns>
        Task StopScheduleAsync();
    }
}
