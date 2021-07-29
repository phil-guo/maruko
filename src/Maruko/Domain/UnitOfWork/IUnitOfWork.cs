using System;

namespace Maruko.Core.Domain.UnitOfWork
{
    /// <summary>
    ///     工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     提交请求
        /// </summary>
        int Commit();
    }
}