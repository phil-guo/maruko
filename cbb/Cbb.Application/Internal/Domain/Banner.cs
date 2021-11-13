using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cbb.Application
{
    [Table(Name = "app_banner")]
    public class Banner : FreeSqlEntity
    {
        [Column(Name = "name", StringLength = 32)]
        public string Name { get; set; }

        [Column(Name = "images", StringLength = 320)]
        public string Images { get; set; }
    }
}