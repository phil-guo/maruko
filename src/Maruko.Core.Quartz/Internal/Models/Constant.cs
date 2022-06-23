namespace Maruko.Core.Quartz.Internal.Models
{
    public class Constant
    {
        /// <summary>
        /// 日志
        /// </summary>
        public const string LOGS = "LOGS";

        /// <summary>
        /// 执行次数
        /// </summary>
        public const string RUNNUMBER = "RunNumber";

        /// <summary>
        /// 是否发送邮件
        /// </summary>
        public const string MAILMESSAGE = "MailMessage";

        public const string JobTypeEnum = "JobTypeEnum";

        /// <summary>
        /// 数据处理接口URL
        /// </summary>
        public const string DATAHANDLER = "DataHandler";

        /// <summary>
        /// 请求url RequestUrl
        /// </summary>
        public const string REQUESTURL = "RequestUrl";

        /// <summary>
        /// 请求类型 RequestType
        /// </summary>
        public const string REQUESTTYPE = "RequestType";

        /// <summary>
        /// 请求参数 RequestParameters
        /// </summary>
        public const string REQUESTPARAMETERS = "RequestParameters";

        /// <summary>
        /// Headers（可以包含：Authorization授权认证）
        /// </summary>
        public const string HEADERS = "Headers";

        //public const string EndAt = "EndAt";

        /// <summary>
        /// 异常 Exception
        /// </summary>
        public const string EXCEPTION = "Exception";

        /// <summary>
        /// 服务唯一标识
        /// </summary>
        public const string AppId = "AppId";

        /// <summary>
        /// request 请求状态
        /// </summary>
        public const string JobRequestStatus = "JobRequestStatus";

        /// <summary>
        /// 下次请求时间
        /// </summary>
        public const string NextRequestTime = "NextRequestTime";
    }
}
