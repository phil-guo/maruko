using System;

namespace Maruko.Core.Domain.Entities
{
    /// <summary>
    ///     主键 <see cref="Entity{TPrimaryKey}" /> long 类型的主键(<see cref="Guid" />).
    /// </summary>
    public abstract class Entity : Entity<long>
    {
        public virtual DateTime CreateTime { get; set; }
    }

    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}