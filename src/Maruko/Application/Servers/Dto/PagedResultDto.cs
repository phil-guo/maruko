namespace Maruko.Application.Servers.Dto
{
    /// <summary>
    /// 分页返回结果
    /// </summary>
    public class PagedResultDto
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        public PagedResultDto(int total, object data)
        {
            TotalCount = total;
            Data = data;
        }
    }
}
