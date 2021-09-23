using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.TaskScheduling
{
    [Table(Name = "task_schedule")]
    public class TaskScheduling : FreeSqlEntity
    {
        [Column(Name = "name", StringLength = 100)]
        public string Name { get; set; }
        [Column(Name = "groupName", StringLength = 100)]
        public string GroupName { get; set; }
        [Column(Name = "status")]
        public int Status { get; set; }
        [Column(Name = "startTime")]
        public DateTime? StartTime { get; set; }
        [Column(Name = "overTime")]
        public DateTime? OverTime { get; set; }
        [Column(Name = "cronExpression", StringLength = 16)]
        public string CronExpression { get; set; }
    }
}
