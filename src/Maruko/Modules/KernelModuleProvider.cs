using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Extensions;

namespace Maruko.Core.Modules
{
    public class KernelModuleProvider : IKernelModuleProvider
    {
        private List<KernelModule> _modules;

        public KernelModuleProvider(List<KernelModule> modules)
        {
            _modules = modules;
        }

        public void Initialize()
        {
            this.TryCatch(() => { _modules.ForEach(item => { item.Initialize(AutofacExtensions.Current, AutofacExtensions.Services); }); }, "模块初始化错误");
        }
    }
}
