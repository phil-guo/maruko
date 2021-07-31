using Autofac;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Maruko.Core.AutoMapper.AutoMapper;
using Maruko.Core.Modules;

namespace Maruko.Core.AutoMapper
{
    public class AutoMapperModule : KernelModule
    {
        protected override void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterAutoMapper(config =>
            {
                config.CreateMappings();
            });
            builder.RegisterType<AutoMapperObjectMapper>().As<Maruko.Core.ObjectMapping.IObjectMapper>().SingleInstance();
        }
    }
}
