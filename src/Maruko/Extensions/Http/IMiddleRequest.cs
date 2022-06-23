using System;
using System.Collections.Generic;

namespace Maruko.Core.Extensions.Http
{
    public interface IMiddleRequest<T> where T : MiddleResponse
    {
        /// <summary>
        /// 系统请求域对应的key
        /// </summary>
        string ServiceDomainKey { get; }

        /// <summary>
        /// 请求路由
        /// </summary>
        string UseRoutName { get; }

        /// <summary>
        /// 重置路由请求
        /// </summary>

        /// <summary>
        /// 服务方式 http Get;Post
        /// <see cref="ServiceMethodRequest"></see>
        /// </summary>
        ServiceMethodRequest ServiceTypeEnum { get; }
        /// <summary>
        /// 需要验证具体用户默认可以获取token
        /// </summary>
        bool AttachToken { get; }
        /// <summary>
        /// 前提是默认token 设置为flase
        /// </summary>
        string OverrideToken => string.Empty;
        /// <summary>
        /// 内部服务默认true 系统自动添加token
        /// </summary>
        bool DefalutEQualityService => true;
        /// <summary>
        /// 所有的键值对对象实现最终数据请求在这里 SseviceMethodRequest 如果是Get 请完善键值对提交数据
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> GetDictionary()
        {
            var dic = new Dictionary<string, object>();
            var type = this.GetType();
            foreach (var item in type.GetProperties())
            {
                string strValue;
                var value = item.GetValue(this,null);
                if (value == null)
                {
                    strValue = null;
                }
                else if (value is string)
                {
                    strValue = (string)value;
                }
                else if (value is DateTime?)
                {
                    var dateTime = value as DateTime?;
                    strValue = dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else if (value is int?)
                {
                    strValue = (value as int?).Value.ToString();
                }
                else if (value is long?)
                {
                    strValue = (value as long?).Value.ToString();
                }
                else if (value is double?)
                {
                    strValue = (value as double?).Value.ToString();
                }
                else if (value is bool?)
                {
                    strValue = (value as bool?).Value.ToString().ToLower();
                }
                else
                {
                    strValue = value.ToString();
                }

                dic.Add(item.Name, strValue);
            }

            return dic;
        }
    }
}