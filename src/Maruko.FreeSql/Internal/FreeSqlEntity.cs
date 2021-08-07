using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DataAnnotations;
using Maruko.Core.Domain.Entities;

namespace Maruko.Core.FreeSql.Internal
{
    public abstract class FreeSqlEntity : Entity, ISoftDelete
    {
        public FreeSqlEntity()
        {
            CreateTime = DateTime.Now;
        }

        [Column(Name = "id", IsIdentity = true, IsPrimary = true)]
        public override long Id { get; set; }

        [Column(Name = "createTime")]
        public override DateTime CreateTime { get; set; }

        [Column(Name = "isDelete")]
        public bool IsDelete { get; set; }
    }
}
