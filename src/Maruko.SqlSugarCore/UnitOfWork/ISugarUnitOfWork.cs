using Maruko.Dependency;
using Maruko.Domain.UnitOfWork;
using SqlSugar;

namespace Maruko.SqlSugarCore.UnitOfWork
{
    public interface ISugarUnitOfWork : IUnitOfWork, IDependencySingleton
    {
        SqlSugarClient SqlSugarClient(bool isEnableLog = false);
    }
}
