using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maruko.Domain.Entities.Auditing
{
    /// <summary>
    /// 审计抽象
    /// </summary>
    /// <typeparam name="TTPrimaryKey">主键</typeparam>
    [Serializable]
    public abstract class FullAuditedEntity<TTPrimaryKey> : /*ModelContext,*/ IEntity<TTPrimaryKey>/*, IHasCreationTime,*/
        //IHasModificationTime, ISoftDelete
    {
//        protected FullAuditedEntity()
//        {
//            CreateTime = DateTime.Now;
//        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TTPrimaryKey Id { get; set; }

        //[NotMapped]
        //public virtual DateTime CreateTime { get; set; }

        //[NotMapped]
        //public virtual DateTime? LastModificationTime { get; set; }

        //[NotMapped]
        //public virtual bool IsDeleted { get; set; }
    }
}
