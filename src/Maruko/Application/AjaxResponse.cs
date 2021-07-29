namespace Maruko.Core.Application
{
    /// <summary>
    /// API 返回对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AjaxResponse<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="msg"></param>
        /// <param name="status"></param>
        public AjaxResponse(T datas, string msg = "", int status = 200)
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
        public AjaxResponse(string msg, int status = 200)
        {
            Msg = msg;
            Status = status;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AjaxResponse()
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
        public int Status { get; set; }
    }
}
