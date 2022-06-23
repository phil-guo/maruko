namespace Maruko.Core.Extensions.Http
{
    public class ModelResponse<T> : MiddleResponse
    {
        /// <summary>
        /// 返回数据 
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 返回信息描述
        /// </summary>
        public string Msg { get; set; } = "失败";

        /// <summary>
        /// 错误编码
        /// </summary>
        public int Code { get; set; } = 500;
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success => Code == 200;
    }
}