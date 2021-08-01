using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.Zero
{
    [Table(Name = "sys_role")]
    public class SysRole : FreeSqlEntity
    {
        [Column(Name = "name", StringLength = 20)]
        public string Name { get; set; }

        [Column(DbType = "TEXT", Name = "remark")]
        public string Remark { get; set; }
    }
}