using Autofac;
using Maruko.Core.Extensions;
using Maruko.Zero;
using Xunit;
using Xunit.Abstractions;

namespace Maruko.Core.Test.Zero
{
    public class OperateTest : TestMarukoCoreBase
    {
        private readonly IOperateService _operate;


        public OperateTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _operate = ServiceLocator.Current.Resolve<IOperateService>();
        }

        [Fact]
        public void GetMenuOfOperate_Test()
        {
            var one = _operate.GetMenuOfOperate(new MenuOfOperateRequest()
            {
                MenuId = 11,
                RoleId = 1
            });

            Print(one);
        }
    }
}