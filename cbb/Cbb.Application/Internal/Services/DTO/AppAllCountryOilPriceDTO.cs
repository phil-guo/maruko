using System.Collections.Generic;
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

    public class OilPrice
    {
        public string Name { get; set; }
        public string Price { get; set; }
    }

    public class Oil
    {
        public string ChangeTime { get; set; }

        public List<OilPrice> OilPrices { get; set; } = new List<OilPrice>();
    }
}