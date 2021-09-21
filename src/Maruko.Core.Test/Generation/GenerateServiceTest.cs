using System.Linq;
using Maruko.CodeGeneration.Generate;
using Maruko.Core.Extensions;
using Maruko.Core.FreeSql.Internal;
using Xunit;
using Xunit.Abstractions;

namespace Maruko.Core.Test.Generation
{
    public class GenerateServiceTest : TestMarukoCoreBase
    {
        public GenerateServiceTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public void GenerateService_Test()
        {
            var one = ContainerBuilderExtensions.ReferenceAssembly.FirstOrDefault(item =>
                item.GetName().Name == "Cbb.Application");
            var types = one?.GetTypes().Where(item => item.BaseType == typeof(FreeSqlEntity))
                .Select(item=>item.Name)
                .ToList();
            CodeGenerate.GenerateService(types);
            CodeGenerate.GenerateController(types);
        }
    }
}