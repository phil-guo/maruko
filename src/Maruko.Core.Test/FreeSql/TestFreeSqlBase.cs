using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.FreeSql;
using Xunit.Abstractions;

namespace Maruko.Core.Test.FreeSql
{
    public class TestFreeSqlBase : TestMarukoCoreBase
    {
        public TestFreeSqlBase(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        
    }
}
