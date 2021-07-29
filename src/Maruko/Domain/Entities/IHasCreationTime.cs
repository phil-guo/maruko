using System;

namespace Maruko.Core.Domain.Entities
{
    /// <summary>
    ///     创建时间
    /// </summary>
    public interface IHasCreationTime
    {
        DateTime CreateTime { get; set; }
    }
}