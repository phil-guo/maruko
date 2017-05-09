using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Domain.Entities.Auditing
{
    public abstract class FullAuditedEntity<TTPrimaryKey> : IEntity<TTPrimaryKey>, IHasCreationTime,
        IHasModificationTime, ISoftDelete
    {
        protected FullAuditedEntity()
        {
            CreateTime = DateTime.Now;
        }
        public TTPrimaryKey Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
