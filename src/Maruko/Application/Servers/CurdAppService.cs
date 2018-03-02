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
            Logger = LogHelper.Log4NetInstance.LogFactory(
                typeof(CurdAppService<TEntity, TPrimaryKey, TCreateEntityDto, TUpdateEntityDto>));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        /// <returns>实体</returns>
        public virtual TEntity Insert(TCreateEntityDto dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        /// <returns>实体</returns>
        public virtual TEntity Update(TUpdateEntityDto dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id">主键</param>
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        public virtual void Delete(TPrimaryKey id)
        {
            Repository.Delete(id);
            Repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// 查询一个实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return Repository.FirstOrDefault(id);
        }

        /// <summary>
        /// 查询一个实体
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FirstOrDefault(predicate);
        }

        /// <summary>
        /// 查询分页数据（倒序排列）
        /// </summary>
        /// <param name="skipCount">页码</param>
        /// <param name="maxResultCount">每页显示数量</param>
        /// <returns></returns>
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
}
