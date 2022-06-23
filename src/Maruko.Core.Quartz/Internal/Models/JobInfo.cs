using System;
using Quartz;

namespace Maruko.Core.Quartz.Internal.Models
{
    //public class JobInfoModel
    //{
    //    /// <summary>
    //    /// 任务组名
    //    /// </summary>
    //    public string GroupName { get; set; }

    //    /// <summary>
    //    /// 任务信息
    //    /// </summary>
    //    public List<JobInfo> Jobs { get; set; } = new List<JobInfo>();
    //}

    public class JobInfoModel
    {
        /// <summary>
        /// 任务组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime? NextFireTime { get; set; }

        /// <summary>
        /// 上次执行时间
        /// </summary>
        public DateTime? PreviousFireTime { get; set; }

        ///// <summary>
        ///// 开始时间
        ///// </summary>
        //public DateTime BeginTime { get; set; }

        ///// <summary>
        ///// 结束时间
        ///// </summary>
        //public DateTime? EndTime { get; set; }

        /// <summary>
        /// 上次执行的异常信息
        /// </summary>
        public string LastErrMsg { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public TriggerState TriggerState { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public string RequestHeader { get; set; }

        /// <summary>
        /// 时间间隔
        /// </summary>
        public string Interval { get; set; }

        /// <summary>
        /// 触发地址
        /// </summary>
        public string TriggerAddress { get; set; }
        public string RequestType { get; set; }
        /// <summary>
        /// 已经执行的次数
        /// </summary>
        public long RunNumber { get; set; }
        public long JobType { get; set; }
        public string DataHandler { get; set; }
        public string AppId { get; set; }
    }
}
