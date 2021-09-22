using System;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.TaskScheduling
{
    [AutoMap(typeof(AllCountryOilPrice))]
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