using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Hangfire;

namespace Maruko.Hangfire.Hangfire
{
    public class HangfireExtension : IHangfireJob
    {
        /// <summary>
        /// 周期任务
        /// </summary>
        /// <param name="methodCall">要执行的方法</param>
        /// <param name="cronExpression">Cron 表达式</param>
        public void AddOrUpdate(Expression<Action> methodCall, Func<string> cronExpression)
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression);
        }


        /// <summary>
        /// 指定任务id的周期任务
        /// </summary>
        /// <param name="recurringJobId">任务id</param>
        /// <param name="methodCall">要执行的方法</param>
        /// <param name="cronExpression">Cron 表达式</param>
        public void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, Func<string> cronExpression)
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression);
        }


        /// <summary>
        /// 删除周期任务
        /// </summary>
        /// <param name="recurringJobId"></param>
        public void Remove(string recurringJobId)
        {
            RecurringJob.RemoveIfExists(recurringJobId);
        }

        /// <summary>
        /// 延时任务
        /// </summary>
        /// <param name="methodCall"></param>
        /// <param name="delay"></param>
        public void Schedule(Expression<Action> methodCall, TimeSpan delay)
        {
            BackgroundJob.Schedule(methodCall, delay);
        }

        /// <summary>
        /// 延时任务
        /// </summary>
        /// <param name="methodCall">有参数的方法</param>
        /// <param name="delay">延时时间</param>
        /// <typeparam name="T"></typeparam>
        public void Enqueue(Expression<Action> methodCall)
        {
            BackgroundJob.Enqueue(methodCall);
        }
    }
}