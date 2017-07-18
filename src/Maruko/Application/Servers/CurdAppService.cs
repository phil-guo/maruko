using System;
using System.Linq;
using System.Linq.Expressions;
using log4net;
using Maruko.Application.Servers.Dto;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.Repositories;
using Maruko.Extensions.Linq;
using Maruko.Logger;

namespace Maruko.Application.Servers
{
    /// <summary>
    /// 增删改查基础实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TCreateEntityDto"></typeparam>
    /// <typeparam name="TUpdateEntityDto"></typeparam>
    public abstract class CurdAppService<TEntity, TPrimaryKey, TCreateEntityDto, TUpdateEntityDto> : ICurdAppService<TEntity, TPrimaryKey, TCreateEntityDto, TUpdateEntityDto>
        where TEntity : FullAuditedEntity<TPrimaryKey>
        where TUpdateEntityDto : EntityDto<TPrimaryKey>
    {
        public readonly IRepository<TEntity, TPrimaryKey> Repository;
        public ILog Logger { get; set; }

        protected CurdAppService(IRepository<TEntity, TPrimaryKey> repository)
        {
            Repository = repository;
        }

        public virtual TEntity Insert(TCreateEntityDto dto)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Update(TUpdateEntityDto dto)
        {
            throw new NotImplementedException();
        }

        public virtual void SoftDelete(TPrimaryKey id)
        {
            var entity = Repository.FirstOrDefault(id);

            if (entity.IsDeleted == false)
                return;

            entity.IsDeleted = true;
            entity.LastModificationTime = DateTime.Now;

            Repository.Update(entity);
            Repository.UnitOfWork.Commit();
        }

        public virtual void Delete(TPrimaryKey id)
        {
            Repository.Delete(id);
            Repository.UnitOfWork.Commit();
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return Repository.FirstOrDefault(id);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FirstOrDefault(predicate);
        }

        public virtual PagedResultDto GetAllByPageList(int skipCount = 0, int maxResultCount = 10)
        {
            var query = Repository
                .GetAll()
                .WhereIf(true, item => item.IsDeleted == false);

            var count = query.Count();

            query = query.OrderByDescending(item => item.CreateTime);
            query = query.PageBy(skipCount, maxResultCount);

            return new PagedResultDto(count, query.ToList());
        }
    }

    public abstract class CurdAppService<TEntity, TCreateEntityDto, TUpdateEntityDto> :
        CurdAppService<TEntity, long, TCreateEntityDto, TUpdateEntityDto>,
        ICurdAppService<TEntity, TCreateEntityDto, TUpdateEntityDto>
        where TEntity : FullAuditedEntity<long>
        where TUpdateEntityDto : EntityDto
    {
        protected CurdAppService(IRepository<TEntity, long> repository)
            : base(repository)
        {
            Logger = LogHelper.Log4NetInstance.LogFactory(
                typeof(CurdAppService<TEntity, TCreateEntityDto, TUpdateEntityDto>));
        }
    }
}
