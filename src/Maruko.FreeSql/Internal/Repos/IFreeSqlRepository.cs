using Maruko.Core.Domain.Entities;
using Maruko.Core.Domain.Repositories;

namespace Maruko.Core.FreeSql.Internal.Repos
{
    public interface IFreeSqlRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        IFreeSql GetAll();
    }
}
