using System;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Cbb.Application
{
    [AutoMap(typeof(AppAllCountryOilPrice))]
    public class OilDTO : EntityDto
    {
        public OilDTO()
        {
            CreateTime = DateTime.Now;
        }

        public string CityName { get; set; }
        public string PriceJson { get; set; }
        public string NextNotify { get; set; }
    }
}