using System;
using AutoMapper;
using Maruko.Extensions;

namespace Maruko.AutoMapper.AutoMapper
{
    /// <summary>
    /// AutoMap 属性映射
    /// </summary>
    public class AutoMapAttribute : AutoMapAttributeBase
    {
        public AutoMapAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {
            if (TargetTypes.IsNullOrEmpty())
            {
                return;
            }

            foreach (var targetType in TargetTypes)
            {
                configuration.CreateMap(type, targetType, MemberList.Source);
                configuration.CreateMap(targetType, type, MemberList.Destination);
            }
        }
    }
}
