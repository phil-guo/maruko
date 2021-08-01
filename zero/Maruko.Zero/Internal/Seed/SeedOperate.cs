using System.Collections.Generic;

namespace Maruko.Zero
{
    public class SeedOperate
    {
        public static List<SysOperate> SeedOperates()
        {
            return new List<SysOperate>
            {
                new SysOperate
                {
                    Id = 1,
                    Name = "添加",
                    Unique = 1001
                },
                new SysOperate
                {
                    Id = 2,
                    Name = "编辑",
                    Unique = 1002
                },
                new SysOperate
                {
                    Id = 3,
                    Name = "查询",
                    Unique = 1003
                },
                new SysOperate
                {
                    Id = 4,
                    Name = "删除",
                    Unique = 1004
                },
                new SysOperate
                {
                    Id = 5,
                    Name = "权限",
                    Unique = 1005
                }
            };
        }
    }
}