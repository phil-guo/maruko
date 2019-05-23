using Autofac;
using AutoMapper;
using Maruko.AutoMapper.AutoMapper;
using Maruko.Modules;
using IObjectMapper = Maruko.ObjectMapping.IObjectMapper;

namespace Maruko.AutoMapper
{
    public class AutoMapperModule : MarukoModule
    {
        public override double Order { get; set; } = 0.3;

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            MapperInitialize.CreateMappings();
            builder.RegisterType<AutoMapperObjectMapper>().As<IObjectMapper>().SingleInstance();
        }
    }
}
