using System.Collections.Generic;

namespace Maruko.Zero
{
    public class SeedRole
    {
        public static List<SysRole> SeedRoles()
        {
            return new List<SysRole>
            {
                new SysRole
                {
                    Id = 1,
                    Name = "管理员"
                }
            };
        }
    }
}