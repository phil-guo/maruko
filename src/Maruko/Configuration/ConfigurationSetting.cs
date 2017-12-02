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
    }
}
