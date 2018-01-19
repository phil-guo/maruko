using System;

namespace Maruko.Domain.Entities.Auditing
{
    /// <summary>
    /// 审计抽象
    /// </summary>
    /// <typeparam name="TTPrimaryKey">主键</typeparam>
    public abstract class FullAuditedEntity<TTPrimaryKey> : IEntity<TTPrimaryKey>, IHasCreationTime,
        IHasModificationTime, ISoftDelete
    {
        protected FullAuditedEntity()
        {
            CreateTime = DateTime.Now;
        }
        public TTPrimaryKey Id { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
