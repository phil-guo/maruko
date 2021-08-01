using Maruko.Core.Application.Servers.Dto;

namespace Maruko.Zero
{
    public class SearchSysUserRequest : PageDto
    { 
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}