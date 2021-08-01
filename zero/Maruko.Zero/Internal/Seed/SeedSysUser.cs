using System.Collections.Generic;
using Maruko.Runtime.Security;

namespace Maruko.Zero
{
    public class SeedSysUser
    {
        public static List<SysUser> SeedSysUsers()
        {
            return new List<SysUser>
            {
                new SysUser
                {
                    Id = 1,
                    UserName = "admin",
                    Password = "123qwe".Get32MD5One(),
                    RoleId = 1
                }
            };
        }
    }
}