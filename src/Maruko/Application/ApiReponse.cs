namespace Maruko.Application
{
    /// <summary>
    /// API 返回对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiReponse<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="msg"></param>
        /// <param name="status"></param>
        public ApiReponse(T datas, string msg = "", ServiceEnum status = ServiceEnum.Success)
        {
            Result = datas;
            Status = status;
            Msg = msg;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="status"></param>
        public ApiReponse(string msg, ServiceEnum status = ServiceEnum.Success)
        {
            Msg = msg;
            Status = status;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiReponse()
        {
        }

        /// <summary>
        /// 返回数据 
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 返回信息描述
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回业务状态
        /// </summary>
        public ServiceEnum Status { get; set; }
    }
}
