using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Cbb.Application
{
    [AutoMap(typeof(AppAllCountryOilPrice))]
    public class AppAllCountryOilPriceDTO : EntityDto
    {
        public string CityName { get; set; }

        public string PriceJson { get; set; }

        public string NextNotify { get; set; }
    }
}