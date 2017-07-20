using Maruko.Dependency;
using Maruko.Domain.Entities;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.UnitOfWork;

namespace Maruko.Domain.Repositories
{
    /// <summary>
    ///     A shortcut of <see cref="IRepository{TEntity,TPrimaryKey}" /> for most used primary key type (<see cref="long" />).
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, long>
        where TEntity : FullAuditedEntity<long>
    {
        
    }
}