using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maruko.Core.Domain.Entities.Auditing
{
    /// <summary>
    /// 审计抽象
    /// </summary>
    /// <typeparam name="TTPrimaryKey">主键</typeparam>
    [Serializable]
    public abstract class FullAuditedEntity<TTPrimaryKey> : IEntity<TTPrimaryKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TTPrimaryKey Id { get; set; }

    }
}
