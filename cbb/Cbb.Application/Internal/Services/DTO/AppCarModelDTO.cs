using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Cbb.Application
{
    [AutoMap(typeof(AppCarModel))]
    public class AppCarModelDTO : EntityDto
    {
        public int Ver { get; set; }
        public long CarModelId { get; set; }
        public long VehicleSystemId { get; set; }
        public long BrandId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
