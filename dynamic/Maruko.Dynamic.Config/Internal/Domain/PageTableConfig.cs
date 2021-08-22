using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.Dynamic.Config
{
    [Table(Name = "dc_page_tableConfig")]
    public class PageTableConfig : FreeSqlEntity
    {
        [Column(Name = "pageId")]
        public long PageId { get; set; }
        [Column(Name = "url", StringLength = 200)]
        public string DataUrl { get; set; }
        [Column(Name = "isRow")]
        public bool IsRow { get; set; }
        [Column(Name = "isMultiple")]
        public bool IsMultiple { get; set; }
        [Column(Name = "rowWith")]
        public int RowWith { get; set; }
    }
}
