using Maruko.Domain.Entities.Auditing;
using Maruko.SqlSugarCore.Reponsitory;
using Maruko.SqlSugarCore.UnitOfWork;

namespace Maruko.SqlSugar.Test
{
    public class TestBaseRepository<TEntity, TPrimaryKey> : MarukoSugarRepository<TEntity, TPrimaryKey>, IMarukoSugarRepository<TEntity, TPrimaryKey>
        where TEntity : FullAuditedEntity<TPrimaryKey>, new()

    {
        public TestBaseRepository(ISugarUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

}
