namespace Maruko.Core.Application.Servers.Dto
{
    /// <summary>
    /// 分页返回结果
    /// </summary>
    public class PagedResultDto
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Datas { get; set; }

        public  PagedResultDto(long total, object data)
        {
            TotalCount = total;
            Datas = data;
        }
    }
}
