using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.UnitOfWork;
using Maruko.EntityFrameworkCore.Repository;

namespace Maruko.Demo.EntityFrameworkCore
{
    public class DemoBaseRepository<TEntity, TPrimaryKey> : MarukoBaseRepository<TEntity, TPrimaryKey>
        where TEntity : FullAuditedEntity<TPrimaryKey>
    {
        public DemoBaseRepository(IDataBaseUnitOfWork unitOfWork, ContextType contextType) 
            : base(unitOfWork, contextType)
        {
        }

        public DemoBaseRepository(IDataBaseUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
