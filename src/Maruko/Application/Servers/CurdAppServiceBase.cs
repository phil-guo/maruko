using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.Domain.Entities;
using Maruko.Core.ObjectMapping;
using Maruko.Domain.Entities;

namespace Maruko.Core.Application.Servers
{
    public abstract class CurdAppServiceBase<TEntity, TPrimaryKey, TEntityDto, TCreateEntityDto, TUpdateEntityDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TUpdateEntityDto : EntityDto<TPrimaryKey>
        where TEntityDto : EntityDto<TPrimaryKey>
    {
        public readonly IObjectMapper ObjectMapper;


        protected CurdAppServiceBase(IObjectMapper objectMapper)
        {
            ObjectMapper = objectMapper;
        }

        protected virtual TEntity MapCreateToEntity(TCreateEntityDto createInput)
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
