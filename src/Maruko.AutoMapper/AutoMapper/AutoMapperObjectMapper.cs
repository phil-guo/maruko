using AutoMapper;
using IObjectMapper = Maruko.ObjectMapping.IObjectMapper;

namespace Maruko.AutoMapper.AutoMapper
{
    public class AutoMapperObjectMapper : IObjectMapper
    {
        //private readonly IMapper _mapper;

        //public AutoMapperObjectMapper(IMapper mapper)
        //{
        //    _mapper = mapper;
        //}

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
