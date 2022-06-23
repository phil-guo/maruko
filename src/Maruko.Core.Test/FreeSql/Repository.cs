using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.Enum;
using Maruko.Core.Extensions;
using Maruko.Core.FreeSql.Internal;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.Test.FreeSql.Model;
using Maruko.Zero;
using Xunit;
using Xunit.Abstractions;

namespace Maruko.Core.Test.FreeSql
{
    public class Repository : TestFreeSqlBase
    {
        private readonly IFreeSqlRepository<Vehicle> _repository;
        private readonly ISysMenuService _menu;
        public Repository(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _repository = ServiceLocator.Current.Resolve<IFreeSqlRepository<Vehicle>>();
            _menu = ServiceLocator.Current.Resolve<ISysMenuService>();
        }

        [Fact]
        public void PageSearch_Test()
        {
            var one = _menu.PageSearch(new PageDto()
            {
                DynamicFilters = new List<DynamicFilter>()
                {
                    new DynamicFilter()
                    {
                        Field = "name",
                        Operate = Condition.Like.ToString(),
                        Value = "角色"
                    }
                }
            });

            Print(one);
        }
       

        [Fact]
        public void GetAllList_Test()
        {
            var one = _repository.GetAllList();

            Print(one);
        }

    }
}
