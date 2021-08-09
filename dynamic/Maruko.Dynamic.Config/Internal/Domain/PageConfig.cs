using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.Dynamic.Config
{
    [Table(Name = "dc_page_config")]
    public class PageConfig : FreeSqlEntity
    {
        [Column(Name = "pageId")]
        public long PageId { get; set; }
        [Column(Name = "fields", StringLength = 1000)]
        public string Fields { get; set; }
        [Column(Name = "buttons", StringLength = 1000)]
        public string Buttons { get; set; }
        [Column(Name = "functions", StringLength = 1000)]
        public string Functions { get; set; }
    }
}
