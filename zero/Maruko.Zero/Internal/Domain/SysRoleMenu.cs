using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.Zero
{
    [Table(Name = "sys_roleMenu")]
    public class SysRoleMenu : FreeSqlEntity
    {
        [Column(Name = "roleId")]
        public long RoleId { get; set; }

        [Column(Name = "menuId")]
        public long MenuId { get; set; }

        [Column(Name = "operates", StringLength = 500)]
        public string Operates { get; set; }
    }
}