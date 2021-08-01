using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.Zero
{
    [Table(Name = "sys_user")]
    public class SysUser : FreeSqlEntity
    {
        [Column(Name = "roleId")]
        public long RoleId { get; set; }

        [Column(Name = "userName", StringLength = 32)]
        public string UserName { get; set; }

        [Column(Name = "password", StringLength = 500)]
        public string Password { get; set; }

        [Column(Name = "icon", StringLength = 500)]
        public string Icon { get; set; }
    }
}