using System;

namespace Maruko.Core.Extensions.Http
{
    /// <summary>
    /// 返回值抽象基类
    /// </summary>
    [Serializable]
    public abstract class MiddleResponse
    {
        /// <summary>
        /// 原始数据内容
        /// </summary>
        public string Body
        {
            get; set;
        }
        public string ErrorMessage { get; set; }
    }
}
