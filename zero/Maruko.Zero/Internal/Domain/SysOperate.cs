using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.Zero
{
    [Table(Name = "sys_operate")]
    public class SysOperate : FreeSqlEntity
    {
        [Column(Name = "name", StringLength = 20)]
        public string Name { get; set; }

        [Column(DbType = "TEXT", Name = "remark")]
        public string Remark { get; set; }

        [Column(Name = "unique")]
        public int Unique { get; set; }

        [Column(Name = "isBasicData")] public bool IsBasicData { get; set; }
    }
}