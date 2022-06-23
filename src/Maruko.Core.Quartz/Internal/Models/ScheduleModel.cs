using System.ComponentModel.DataAnnotations;

namespace Maruko.Core.Quartz.Internal.Models
{
    public class ScheduleModel
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// 全局唯一标识
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public JobTypeEnum JobType { get; set; } = JobTypeEnum.Url;
        ///// <summary>
        ///// 开始时间
        ///// </summary>
        //public DateTimeOffset BeginTime { get; set; } = DateTime.Now;
        ///// <summary>
        ///// 结束时间
        ///// </summary>
        //public DateTime? EndTime { get; set; }
        /// <summary>
        /// Cron表达式
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Cron { get; set; }
        ///// <summary>
        ///// 执行次数（默认无限循环）
        ///// </summary>
        //public int? RunTimes { get; set; }
        ///// <summary>
        ///// 执行间隔时间，单位秒（如果有Cron，则IntervalSecond失效）
        ///// </summary>
        //public int? IntervalSecond { get; set; }

        /// <summary>
        /// 触发器类型
        /// </summary>
        public TriggerTypeEnum TriggerType { get; set; } = TriggerTypeEnum.Cron;
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }

        public MailMessageEnum MailMessage { get; set; }

        /// <summary>
        /// 请求url
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string RequestUrl { get; set; }
        /// <summary>
        /// 请求参数（Post，Put请求用）
        /// </summary>
        public string RequestParameters { get; set; }
        public string DataHandler { get; set; }
        /// <summary>
        /// Headers(可以包含如：Authorization授权认证)
        /// 格式：{"Authorization":"userpassword.."}
        /// </summary>
        //public string Headers { get; set; }
        public string RequestHeader { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public RequestTypeEnum RequestType { get; set; }
    }
}
