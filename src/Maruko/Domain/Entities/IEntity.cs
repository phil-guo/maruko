using System;

namespace Maruko.Core.Domain.Entities
{
    /// <summary>
    ///     long类型的主键
    /// </summary>
    public interface IEntity : IEntity<long>
    {
    }
}