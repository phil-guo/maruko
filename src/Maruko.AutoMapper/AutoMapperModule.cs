using Autofac;
using AutoMapper;
using Maruko.Core.AutoMapper.AutoMapper;
using Maruko.Core.Modules;

namespace Maruko.Core.AutoMapper
{
    public class AutoMapperModule : KernelModule
    {
        protected override void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterType<AutoMapperObjectMapper>().As<Maruko.Core.ObjectMapping.IObjectMapper>().SingleInstance();
        }
    }
}
