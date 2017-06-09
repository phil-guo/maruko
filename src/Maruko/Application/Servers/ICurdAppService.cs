using System;
using System.Linq.Expressions;
using Maruko.Application.Servers.Dto;
using Maruko.Dependency;
using Maruko.Domain.Entities.Auditing;

namespace Maruko.Application.Servers
{
    public interface ICurdAppService<TEntity, in TPrimaryKey>
        where TEntity : FullAuditedEntity<TPrimaryKey>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        TEntity Insert<TCreateEntityDto>(TCreateEntityDto dto);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        TEntity Update<TUpdateEntityDto>(TUpdateEntityDto dto)
            where TUpdateEntityDto : EntityDto;


        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        void SoftDelete(TPrimaryKey id);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="id"></param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// 根据id 获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        /// 根据条件获取一个实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 分页查询
        /// 默认倒序，根据创建时间
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        PagedResultDto GetAllByPageList(int skipCount = 0, int maxResultCount = 10);
    }

    public interface ICurdAppService<TEntity> : ICurdAppService<TEntity, long>, IDependencyTransient
        where TEntity : FullAuditedEntity<long>
    {
    }
}
