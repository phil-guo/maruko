using Maruko.Application.Servers;
using Maruko.Application.Servers.Dto;
using Maruko.AutoMapper.AutoMapper;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.Repositories;

namespace Maruko.Demo.Application
{
    public class DemoBaseCurdApp<TEntity, TCreateEntityDto, TUpdateEntityDto> : CurdAppService<TEntity, long, TCreateEntityDto, TUpdateEntityDto>
        where TEntity : FullAuditedEntity<long>
        where TUpdateEntityDto : EntityDto
    {
        //public DemoBaseCurdApp(IRepository<TEntity, long> repository)
        //    : base(repository)
        //{
        //}

        /// <summary>
        /// 添加一条数据
        /// </summary>
        public override TEntity Insert(TCreateEntityDto dto)
        {
            var entity = dto.MapTo<TEntity>();

            entity = Repository.Insert(entity);
            Repository.UnitOfWork.Commit();

            return entity;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        public override TEntity Update(TUpdateEntityDto dto)
        {
            var entity = FirstOrDefault(item => item.Id == dto.Id);

            var newEntity = dto.MapTo(entity);

            newEntity = Repository.Update(newEntity);
            Repository.UnitOfWork.Commit();

            return newEntity;
        }
    }
}
