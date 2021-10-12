using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Core.Web
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public class SwaggerFileUploadAttribute : Attribute
    {
        /// <summary>
        /// 是否必须
        /// </summary>
        public bool Required { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Required"></param>
        public SwaggerFileUploadAttribute(bool Required = true)
        {
            this.Required = Required;
        }
    }
}
