using System;

namespace Maruko.Domain.Entities.Auditing
{
    /// <summary>
    ///     审计抽象
    /// </summary>
    public abstract class FullAuditedEntity : Entity, IEntity, IHasCreationTime, IHasModificationTime, ISoftDelete
    {
        protected FullAuditedEntity()
        {
            CreateTime = DateTime.Now;
        }

        public DateTime CreateTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    
}