using System;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.TaskScheduling
{
    [AutoMap(typeof(TaskScheduling))]
    public class TaskSchedulingDTO : EntityDto
    {
        public string Name { get; set; }
        public string GroupName { get; set; }
        public bool Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? OverTime { get; set; }
        public string CronExpression { get; set; }
    }
}