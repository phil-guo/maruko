namespace Maruko.Core.Quartz.Internal.Models
{
    public class LogUrlMsg : LogReveal
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public string RequestType { get; set; }
    }
}
