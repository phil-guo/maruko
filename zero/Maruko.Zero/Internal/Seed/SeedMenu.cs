using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maruko.Zero
{
    public class SeedMenu
    {
        public static List<SysMenu> SeedMenus()
        {
            return new List<SysMenu>
            {
                new SysMenu
                {
                    Id = 1,
                    ParentId = 99999,
                    Level = 1,
                    Name = "基础数据管理",
                    Operates = JsonConvert.SerializeObject(new List<int>()),
                    Sort = 0,
                    Url = "/",
                    Icon = "folder-o"
                },
                new SysMenu
                {
                    Id = 2,
                    ParentId = 1,
                    Level = 2,
                    Name = "按钮管理",
                    Url = "operate",
                    Operates = JsonConvert.SerializeObject(new List<int> {1, 2, 3, 4}),
                    Sort = 1,
                    Icon = "folder-o"
                },
                new SysMenu
                {
                    Id = 3,
                    ParentId = 1,
                    Level = 2,
                    Name = "角色管理",
                    Url = "role",
                    Operates = JsonConvert.SerializeObject(new List<int> {1, 2, 3, 4, 5}),
                    Sort = 2,
                    Icon = "folder-o"
                },
                new SysMenu
                {
                    Id = 4,
                    ParentId = 1,
                    Level = 2,
                    Name = "菜单管理",
                    Url = "menu",
                    Operates = JsonConvert.SerializeObject(new List<int> {1, 2, 3, 4}),
                    Sort = 3,
                    Icon = "folder-o"
                },
                new SysMenu
                {
                    Id = 5,
                    ParentId = 1,
                    Level = 2,
                    Name = "系统用户",
                    Url = "sysUser",
                    Operates = JsonConvert.SerializeObject(new List<int> {1, 2, 3, 4}),
                    Sort = 4,
                    Icon = "folder-o"
                }
            };
        }
    }
}