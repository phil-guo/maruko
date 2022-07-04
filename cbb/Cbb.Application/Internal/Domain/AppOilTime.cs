using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Cbb.Application.Internal.Domain
{
    [Table(Name = "app_oil_time")]
    public class AppOilTime : FreeSqlEntity
    {
        /// <summary>
        /// 年度
        /// </summary>
        [Column(Name = "year")]
        public int Year { get; set; }

        [Column(Name = "time")]
        public DateTime Time { get; set; }

        [Column(Name = "sort")]
        public int Sort { get; set; }
    }
}
