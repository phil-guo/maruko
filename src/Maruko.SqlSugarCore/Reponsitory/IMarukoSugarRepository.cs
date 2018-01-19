using Maruko.Dependency;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.Repositories;
using Maruko.SqlSugarCore.UnitOfWork;
using SqlSugar;

namespace Maruko.SqlSugarCore.Reponsitory
{
    public interface IMarukoSugarRepository<TEntity, in TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IDependencyScoped
        where TEntity : FullAuditedEntity<TPrimaryKey>
    {
        ISugarUnitOfWork SugarUnitOfWork { get; }
        //ISugarQueryable<TEntity> SugarQuery();
    }
}
