using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Core.Modules
{
    public interface IKernelModuleProvider
    {
        void Initialize(IApplicationBuilder app);
        void ConfigureServices(IServiceCollection collection);
    }
}
