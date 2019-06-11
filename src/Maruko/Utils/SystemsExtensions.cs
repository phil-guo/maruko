using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Utils
{
    /// <summary>
    /// 框架自定义扩展方法
    /// </summary>
    public static class SystemsExtensions
    {
        /// <summary>
        /// 列表返回数据转换成字典
        /// </summary>
        /// <typeparam name="T">数据对象</typeparam>
        /// <param name="datas">数据</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        public static Dictionary<string, object> DataToDictionary<T>(this T datas, int total)
        {
            return new Dictionary<string, object>()
            {
                { "datas",datas},
                { "total",total}
            };
        }
    }
}
