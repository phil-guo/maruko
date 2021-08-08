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
