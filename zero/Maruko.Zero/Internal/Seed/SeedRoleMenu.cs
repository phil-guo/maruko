using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maruko.Zero
{
    public class SeedRoleMenu
    {
        public static List<SysRoleMenu> SeedRoleMenus()
        {
            return new List<SysRoleMenu>
            {
                new SysRoleMenu
                {
                    Id = 1,
                    MenuId = 1,
                    RoleId = 1,
                    Operates = JsonConvert.SerializeObject(new List<int>())
                },
                new SysRoleMenu
                {
                    Id = 2,
                    MenuId = 2,
                    RoleId = 1,
                    Operates = JsonConvert.SerializeObject(new List<int> {1, 2, 3, 4})
                },
                new SysRoleMenu
                {
                    Id = 3,
                    MenuId = 3,
                    RoleId = 1,
                    Operates = JsonConvert.SerializeObject(new List<int> {1, 2, 3, 4, 5})
                },
                new SysRoleMenu
                {
                    Id = 4,
                    MenuId = 4,
                    RoleId = 1,
                    Operates = JsonConvert.SerializeObject(new List<int> {1, 2, 3, 4})
                },
                new SysRoleMenu
                {
                    Id = 5,
                    MenuId = 5,
                    RoleId = 1,
                    Operates = JsonConvert.SerializeObject(new List<int> {1, 2, 3, 4})
                }
            };
        }
    }
}