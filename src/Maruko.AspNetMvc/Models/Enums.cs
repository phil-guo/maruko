using System.ComponentModel.DataAnnotations;

namespace Maruko.AspNetMvc.Models
{
    /// <summary>
    /// 版本枚举
    /// </summary>
    public enum ApiVersions
    {
        [Display(Name = "V1", Description = "版本一")]
        V1,
        [Display(Name = "V2", Description = "版本二")]
        V2
    }

    /// <summary>
    /// 接口验证方式
    /// </summary>
    public enum AuthorizationType
    {
        /// <summary>
        /// jwt验证
        /// </summary>
        JWT
    }
}
