using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.ObjectMapping;
using Maruko.Core.Test.AutoMap.Model;
using Xunit;
using Xunit.Abstractions;

namespace Maruko.Core.Test.AutoMap
{
    public class MapperTest : TestAutoMapperBase
    {
        private readonly IObjectMapper _objectMapper;
        public MapperTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _objectMapper = ServiceLocator.Current.Resolve<IObjectMapper>();
        }

        [Fact]
        public void Map_Test()
        {
            var modelA = new ModelA()
            {
                Name = "simple",
                Age = 29
            };

            var modelB = _objectMapper.Map<ModelB>(modelA);

            Print(modelB); 
        }
    }
}
