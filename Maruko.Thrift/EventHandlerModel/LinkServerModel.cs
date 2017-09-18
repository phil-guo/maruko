using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Thrift.EventHandlerModel
{
    public class LinkServerModel
    {
        /// <summary>
        /// json 数据参数
        /// </summary>
        public string JsonFormate { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// zookeeper ip地址
        /// </summary>
        public string ZKIpAddress { get; set; }
    }
}
