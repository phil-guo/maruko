using Maruko.Domain.UnitOfWork;
using Maruko.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Maruko.Demo.EntityFrameworkCore
{
    public class DemoEfCoreBaseUow : EfCoreBaseUnitOfWork<DemoDbContext>
    {
        public override DbSet<TEntity> CreateSet<TEntity, TPrimaryKey>(ContextType contextType)
        {
            if (contextType == ContextType.DefaultContextType)
            {
                DefaultDbContext = new DemoDbContext();
                return DefaultDbContext.CreateSet<TEntity, TPrimaryKey>();
            }
            else
            {
                Logger.Debug("不存在这样的ContextType");
                return null;
            }
        }
    }
}
