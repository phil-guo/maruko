using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.Dynamic.Config
{
    [Table(Name = "dc_page_table_fieldConfig")]
    public class PageFieldConfig : FreeSqlEntity
    {
        [Column(Name = "pageTableId")]
        public long PageTableId { get; set; }
        [Column(Name = "name",StringLength = 56)]
        public string Name { get; set; }
        [Column(Name = "field", StringLength = 64)]
        public string Field { get; set; }
        [Column(Name = "url", StringLength = 64)]
        public string Url { get; set; }
        [Column(Name = "isQuery")]
        public bool IsQuery { get; set; }
        [Column(Name = "width")]
        public int Width { get; set; }
        [Column(Name = "minWidth")]
        public int MinWidth { get; set; }
        [Column(Name = "isAdd")]
        public bool IsAdd { get; set; }
        [Column(Name = "isEdit")]
        public bool IsEdit { get; set; }
        [Column(Name = "condition",StringLength = 10)]
        public string Condition { get; set; }
    }
}
