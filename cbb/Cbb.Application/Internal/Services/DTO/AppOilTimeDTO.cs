using System;
using System.Collections.Generic;
using System.Text;
using Cbb.Application.Internal.Domain;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Cbb.Application.Internal.Services.DTO
{
    [AutoMap(typeof(AppOilTime))]
    public class AppOilTimeDTO : EntityDto
    {
        /// <summary>
        /// 年度
        /// </summary>
        public int Year { get; set; }

        public DateTime Time { get; set; }

        public int Sort { get; set; }
    }
}
