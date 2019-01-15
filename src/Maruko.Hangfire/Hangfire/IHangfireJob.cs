using System;
using System.Linq.Expressions;
using Maruko.Attributes;
using Maruko.Dependency;

namespace Maruko.Hangfire.Hangfire
{
    public interface IHangfireJob : IDependencyTransient
    {
        /// <summary>
        /// 周期任务
        /// </summary>
        /// <param name="methodCall">要执行的方法</param>
        /// <param name="cronExpression">Cron 表达式</param>
        void AddOrUpdate(Expression<Action> methodCall, Func<string> cronExpression);

        /// <summary>
        /// 指定任务id的周期任务
        /// </summary>
        /// <param name="recurringJobId">任务id</param>
        /// <param name="methodCall">要执行的方法</param>
        /// <param name="cronExpression">Cron 表达式</param>
        void AddOrUpdate(string recurringJobId, Expression<Action> methodCall, Func<string> cronExpression);

        /// <summary>
        /// 删除周期任务
        /// </summary>
        /// <param name="recurringJobId"></param>
        void Remove(string recurringJobId);


        /// <summary>
        /// 延时任务
        /// </summary>
        /// <param name="methodCall"></param>
        /// <param name="delay"></param>
        void Schedule(Expression<Action> methodCall, TimeSpan delay);

        /// <summary>
        /// 任务队列
        /// </summary>
        void Enqueue(Expression<Action> methodCall);
    }
}
