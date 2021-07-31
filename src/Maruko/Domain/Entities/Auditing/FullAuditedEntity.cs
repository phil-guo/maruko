using System;

namespace Maruko.Core.Domain.Entities.Auditing
{
    /// <summary>
    ///     审计抽象<see cref="Entity{TPrimaryKey}" /> long 类型的主键(<see cref="long" />).
    /// </summary>
    public abstract class FullAuditedEntity : Entity, IEntity, IHasCreationTime, IHasModificationTime, ISoftDelete
    {
        protected FullAuditedEntity()
        {
            CreateTime = DateTime.Now;
        }

        public DateTime CreateTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}