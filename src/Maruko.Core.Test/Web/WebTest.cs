using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.Modules;
using Xunit;
using Xunit.Abstractions;

namespace Maruko.Core.Test.Web
{
    public class WebTest:TestMarukoCoreBase
    {
        public WebTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public void Initialize_Test()
        {
            var kernelModule = ServiceLocator.Current.Resolve<IKernelModuleProvider>();
            kernelModule.Initialize();
        }
    }
}
