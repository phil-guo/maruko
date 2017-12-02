using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.MongoDB.MongoDBRepos
{
    public class MongodbSettings
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// database 名称
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 登录数据库
        /// </summary>
        public string LoginDatabase { get; set; }
    }
}
