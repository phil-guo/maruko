using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Core.Config
{
    public class CoreOption
    {
        /// <summary>
        /// 排除的模块
        /// </summary>
        public string ExcludeModules { get; set; }

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string Connection { get; set; }
    }
}
