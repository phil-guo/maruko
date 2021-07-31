using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;
using Maruko.Core.Test.FreeSql.Model;

namespace Maruko.Core.Test.FreeSql.DTO
{
    [AutoMap(typeof(Vehicle))]
    public class VehicleDTO : EntityDto
    {
        public string Number { get; set; }

        public string LoadCapacity { get; set; }

        /// <summary>
        /// 0.可用 1.不可用(运送中)
        /// </summary>
        public string CurrentState { get; set; }

        /// <summary>
        /// 0.小型1.中型2.大型
        /// </summary>
        public string Type { get; set; }

        public bool IsComplete { get; set; }
    }
}
