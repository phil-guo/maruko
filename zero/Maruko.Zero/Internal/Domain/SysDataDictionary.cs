using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.Zero
{
    [Table(Name = "sys_dataDictionary")]
    public class SysDataDictionary : FreeSqlEntity
    {
        [Column(Name = "key", StringLength = 32)]
        public string Key { get; set; }

        [Column(Name = "value", StringLength = 32)]
        public string Value { get; set; }

        [Column(Name = "group", StringLength = 32)]
        public string Group { get; set; }

        [Column(Name = "isBasicData")] public bool IsBasicData { get; set; }
    }
}