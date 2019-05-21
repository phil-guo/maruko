namespace Maruko.AspNetMvc
{

    public static class AspNetMvcGlobal
    {

        public static string PaysChannelKey = "channel";

        /// <summary>
        /// 商户名称集合key
        /// </summary>
        public const string MerMaterialNames = "mer_material_names";

        /// <summary>
        /// 工作日配置
        /// </summary>
        public const string WorkDayDeploy = "WorkDayDeploy";

        public static string PollingKey = "polling";

        /// <summary>
        /// token缓存key
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static string TokenCacheKey(string cacheKey)
        {
            return $"token_{cacheKey}";
        }


        /// <summary>
        /// 商户的缓存key
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static string MerCacheKey(string cacheKey)
        {
            return $"pay_merchant_{cacheKey}";
        }

        /// <summary>
        /// 商户余额key
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static string MerBalanceKey(string cacheKey)
        {
            return $"balance_mer_{cacheKey}";
        }

        public static string MerLedgersKey(int cacheKey)
        {
            return $"ledger_mer_{cacheKey}";
        }

        /// <summary>
        /// 用户通道缓存key
        /// </summary>
        /// <param name="cacheKey">用户ID_产品ID</param>
        /// <returns></returns>
        public static string UserChannelCacheKey(string cacheKey)
        {
            return $"pay_uchannel_{cacheKey}";
        }

        /// <summary>
        /// 系统通道缓存key
        /// </summary>
        /// <param name="cacheKey">产品ID</param>
        /// <returns></returns>
        public static string SysChannelCacheKey(string cacheKey)
        {
            return $"pay_syschannel_{cacheKey}";
        }
    }
}
