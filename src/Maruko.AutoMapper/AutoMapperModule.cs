using Autofac;
using AutoMapper;
using Maruko.AutoMapper.AutoMapper;
using Maruko.Core.Modules;
using IObjectMapper = Maruko.Core.ObjectMapping.IObjectMapper;

namespace Maruko.AutoMapper
{
    public class AutoMapperModule : KernelModule
    {
        

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            MapperInitialize.CreateMappings();
            builder.RegisterType<AutoMapperObjectMapper>().As<IObjectMapper>().SingleInstance();
        }
    }
}
