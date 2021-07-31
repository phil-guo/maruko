using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maruko.Core.Application.Servers.Dto
{
    public abstract class PageDto
    {
        /// <summary>
        /// 动态查询条件
        /// </summary>
        public List<DynamicFilter> DynamicFilters { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        [Range(1, 2147483647, ErrorMessage = "PageIndex的值在1~2147483647之间")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 一页数据总数
        /// </summary>
        [Range(1, 2147483647, ErrorMessage = "PageSize的值在1~2147483647之间")]
        public int PageMax { get; set; } = 20;
    }
}
