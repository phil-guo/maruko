using System;

namespace Maruko.Domain.Entities
{
    /// <summary>
    ///     最后修改时间
    /// </summary>
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; set; }
    }
}