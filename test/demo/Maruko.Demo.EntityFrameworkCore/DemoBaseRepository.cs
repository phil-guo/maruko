using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.UnitOfWork;
using Maruko.EntityFrameworkCore.Repository;
using Maruko.EntityFrameworkCore.UnitOfWork;

namespace Maruko.Demo.EntityFrameworkCore
{
    public class DemoBaseRepository<TEntity, TPrimaryKey> : MarukoBaseRepository<TEntity, TPrimaryKey>
        where TEntity : FullAuditedEntity<TPrimaryKey>
    {
        public DemoBaseRepository(IEfUnitOfWork unitOfWork, ContextType contextType) 
            : base(unitOfWork, contextType)
        {
        }

        public DemoBaseRepository(IEfUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
