using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Xunit;
using Xunit.Abstractions;

namespace Maruko.Core.Test.Web
{
    public class WebTest : TestMarukoCoreBase
    {
        private readonly IApplicationBuilder _app;
        public WebTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _app = ServiceLocator.Current.Resolve<IApplicationBuilder>();
        }

        [Fact]
        public void Initialize_Test()
        {
            var kernelModule = ServiceLocator.Current.Resolve<IKernelModuleProvider>();
            kernelModule.Initialize(_app);
        }
    }
}
