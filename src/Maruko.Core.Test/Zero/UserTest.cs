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
    public class UserTest : TestMarukoCoreBase
    {
        private readonly ISysUserService _user;

        public UserTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _user = ServiceLocator.Current.Resolve<ISysUserService>();
        }

        [Fact]
        public void Login_Test()
        {
            var one = _user.Login(new LoginVM()
            {
                Name = "admin",
                Password = "123qwe"
            });

            Print(one);
        }

    }
}
