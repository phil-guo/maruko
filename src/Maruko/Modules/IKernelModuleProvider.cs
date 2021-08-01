using Microsoft.AspNetCore.Builder;

namespace Maruko.Core.Modules
{
    public interface IKernelModuleProvider
    {
        void Initialize(IApplicationBuilder app);
    }
}
