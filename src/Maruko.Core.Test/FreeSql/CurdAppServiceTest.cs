using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.Test.FreeSql.AppService;
using Xunit;
using Xunit.Abstractions;

namespace Maruko.Core.Test.FreeSql
{
    public class CurdAppServiceTest : TestFreeSqlBase
    {
        private readonly IVehicleAppService _vehicle;
        public CurdAppServiceTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _vehicle = ServiceLocator.Current.Resolve<IVehicleAppService>();
        }

        [Fact]
        public void GetAllList_Test()
        {
            var one = _vehicle.FirstOrDefault(1);
            Print(one);
        }
    }
}
