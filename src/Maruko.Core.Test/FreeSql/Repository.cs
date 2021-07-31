using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.Test.FreeSql.Model;
using Xunit;
using Xunit.Abstractions;

namespace Maruko.Core.Test.FreeSql
{
    public class Repository : TestFreeSqlBase
    {
        private readonly IFreeSqlRepository<Vehicle> _repository;
        public Repository(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _repository = ServiceLocator.Current.Resolve<IFreeSqlRepository<Vehicle>>();
        }

       

        [Fact]
        public void GetAllList_Test()
        {
            var one = _repository.GetAllList();

            Print(one);
        }

    }
}
