using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.Extensions;
using Maruko.Core.FreeSql.Internal;
using Maruko.Core.Test.FreeSql.AppService;
using Maruko.Core.Test.FreeSql.DTO;
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
        public void Delete_Test()
        {
            _vehicle.Delete(1);

            var one = _vehicle.FirstOrDefault(1);

            Print(one);
        }

        [Fact]
        public void PageSearch_Test()
        {
            var one = _vehicle.PageSearch(new SearchVehicle()
            {
                DynamicFilters = new List<DynamicFilter>()
                {
                    //new DynamicFilter()
                    //{
                    //    Field = "Number",
                    //    Operate = Condition.Equal.ToString(),
                    //    Value = "渝A·F5671"
                    //},
                    //new DynamicFilter()
                    //{
                    //    Field = "LoadCapacity",
                    //    Operate = Condition.Like.ToString(),
                    //    Value = "200"
                    //},

                    //new DynamicFilter()
                    //{
                    //    Field = "Age",
                    //    Operate = Condition.Like.ToString(),
                    //    Value = 20
                    //},
                    new DynamicFilter()
                    {
                        Field = "CreateTime",
                        Operate = Condition.GreaterThanOrEqual.ToString(),
                        Value = DateTime.Now
                    },
                }
            });

            Print(one);
        }

        [Fact]
        public void FirstOrDefault_Test()
        {
            var one = _vehicle.FirstOrDefault(1);
            Print(one);
        }
    }
}
