using Microsoft.Extensions.Configuration;

namespace Maruko.Configuration
{
    /// <summary>
    /// 配置文件设置
    /// </summary>
    public static class ConfigurationSetting
    {
        /// <summary>
        /// 配置管理上下文
        /// </summary>
        public static IConfiguration DefaultConfiguration { get; set; }
        
        public static string GetAppSetting(string key)
        {
            string str = string.Empty;
            if (DefaultConfiguration.GetSection(key) != null)
                str = DefaultConfiguration.GetSection(key).Value;
            return str;
        }
        
        public static void SetAppSetting(IConfigurationSection section) => DefaultConfiguration = section;
    }
}
