namespace Maruko.Application.Servers.Dto
{
    public class PagedResultDto
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        public object Data { get; set; }

        public PagedResultDto(int total, object data)
        {
            TotalCount = total;
            Data = data;
        }
    }
}
