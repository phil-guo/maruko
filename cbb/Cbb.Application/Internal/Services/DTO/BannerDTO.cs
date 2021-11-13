using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Cbb.Application
{
    [AutoMap(typeof(Banner))]
    public class BannerDTO : EntityDto
    {
        public string Name { get; set; }

        public string Images { get; set; }
    }
}