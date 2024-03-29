﻿using System;
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
        [Column(Name = "url", StringLength = 200)]
        public string DataUrl { get; set; }
        [Column(Name = "isRow")]
        public bool IsRow { get; set; }
        [Column(Name = "isMultiple")]
        public bool IsMultiple { get; set; }
        [Column(Name = "rowWith")]
        public int RowWith { get; set; }
        [Column(Name = "fields",DbType = "text")]
        public string Fields { get; set; }
        [Column(Name = "buttons",DbType = "text")]
        public string Buttons { get; set; }
        [Column(Name = "functions",DbType = "text")]
        public string Functions { get; set; }
        [Column(Name = "isTableOperateRow")]
        public bool IsTableOperateRow { get; set; }
        [Column(Name = "IsTableCheckRow")]
        public bool IsTableCheckRow { get; set; }
    }
}
