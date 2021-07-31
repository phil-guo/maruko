using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.AutoMapper;
using Xunit.Abstractions;

namespace Maruko.Core.Test.AutoMap
{
    public class TestAutoMapperBase : TestMarukoCoreBase
    {
        public TestAutoMapperBase(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        protected override void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterModule<AutoMapperModule>();
        }
    }
}
