using System;
using Maruko.Configuration;
using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;

namespace Maruko.Redis.Runtime
{
    public class RedisManager
    {
        public static RedisManager Instance = new RedisManager();


        private IRedisClient RedisClient { get; set; }

       

        public RedisManager RedisLink()
        {
            var manager = new RedisManagerPool(GetRedisConnStr());
            var client = manager.GetClient();
            RedisClient = client;

            return Instance;
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, string value)
        {
            using (var client = RedisClient)
            {
                return client.Set(key, value);
            }
        }

        /// <summary>
        /// 设置有过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public bool Set(string key, string value, TimeSpan timeSpan)
        {
            using (RedisClient)
            {
                return RedisClient.Set(key, value, timeSpan);
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            using (var client = RedisClient)
            {
                return client.Get<string>(key);
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            using (RedisClient)
            {
                return RedisClient.Remove(key);
            }
        }

        private string GetRedisConnStr()
        {
            return ConfigurationSetting.DefaultConfiguration.GetConnectionString("RedisConnection");
        }
    }
}
