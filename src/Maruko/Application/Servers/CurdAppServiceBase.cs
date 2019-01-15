using log4net;
using Maruko.Application.Servers.Dto;
using Maruko.Domain.Entities;
using Maruko.Logger;
using Maruko.ObjectMapping;

namespace Maruko.Application.Servers
{
    public abstract class CurdAppServiceBase<TEntity, TPrimaryKey, TEntityDto, TCreateEntityDto, TUpdateEntityDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TUpdateEntityDto : EntityDto<TPrimaryKey>
        where TEntityDto : EntityDto<TPrimaryKey>
    {
        public readonly IObjectMapper ObjectMapper;

        public ILog Logger { get; set; }

        protected CurdAppServiceBase(IObjectMapper objectMapper)
        {
            ObjectMapper = objectMapper;
            //ObjectMapper = NullObjectMapper.Instance;
            Logger = LogHelper.Log4NetInstance.LogFactory(
                typeof(CurdAppService<TEntity, TPrimaryKey, TEntityDto, TCreateEntityDto, TUpdateEntityDto>));
        }

        protected virtual TEntity MapToEntity(TCreateEntityDto createInput)
        {
            return ObjectMapper.Map<TEntity>(createInput);
        }
        protected virtual void MapToEntity(TUpdateEntityDto updateInput, TEntity entity)
        {
            ObjectMapper.Map(updateInput, entity);
        }
        protected virtual TEntityDto MapToEntityDto(TEntity entity)
        {
            return ObjectMapper.Map<TEntityDto>(entity);
        }

        protected virtual TEntity MapToEntity(TEntityDto entityDto)
        {
            return ObjectMapper.Map<TEntity>(entityDto);
        }
    }
}
