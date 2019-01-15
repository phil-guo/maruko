using System;
using Maruko.Dependency;

namespace Maruko.Domain.UnitOfWork
{
    /// <summary>
    ///     工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable, IDependencyTransient
    {
        /// <summary>
        ///     提交请求
        /// </summary>
        int Commit();

        /// <summary>
        ///     提交所有请求并处理乐观锁的问题
        /// </summary>
        void CommitAndRefreshChanges();

        /// <summary>
        ///     回滚所有的请求
        /// </summary>
        void RollbackChanges();
    }
}