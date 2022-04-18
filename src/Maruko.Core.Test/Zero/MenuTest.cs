using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Zero;
using Xunit;
using Xunit.Abstractions;

namespace Maruko.Core.Test.Zero
{
    public class MenuTest : TestMarukoCoreBase
    {
        private readonly ISysMenuService _menu;
        public MenuTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _menu = ServiceLocator.Current.Resolve<ISysMenuService>();
        }

        [Fact]
        public void CreateOrEdit_Test()
        {
            _menu.CreateOrEdit(new SysMenuDTO()
            {
                Name = "系统菜单1",
                Level = 2,
                ParentId = 1,
                Icon = "el-icon-loading",
                Url = "/system/menu",
                Key = "",
                Operates = "[2,1,3,4]",
                IsLeftShow = true,
                OperateModels = new List<OperateModel>(),
                Id = 22
            });
        }

        [Fact]
        public void GetMenusSetRole_Test()
        {
            var one = _menu.GetMenusSetRole(new MenusRoleRequest() { RoleId = 1 });

            Print(one);
        }

        [Fact]
        public void GetMenusByRole_Test()
        {
            var one = _menu.GetMenusByRole(new MenusRoleRequest()
            {
                RoleId = 1
            });

            Print(one);
        }
    }
}
