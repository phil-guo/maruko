using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Cbb.Application
{
    [AutoMap(typeof(AppCarBrand))]
    public class AppCarBrandDTO : EntityDto
    {
        public int Ver { get; set; }

        public long BrandId { get; set; }

        public string Name { get; set; }

        public string FirstLetter { get; set; }
    }
}
