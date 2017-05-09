using System;
using System.Linq.Expressions;
using Hangfire;

namespace Maruko.Hangfire.Hangfire
{
    public static class HangfireManager
    {
        /// <summary>
        /// 定时任务
        /// </summary>
        /// <param name="methodCall"></param>
        /// <param name="cron"></param>
        public static void TimingTask(Expression<Action> methodCall, string cron)
        {
            RecurringJob.AddOrUpdate(methodCall, cron);
        }
    }
}
