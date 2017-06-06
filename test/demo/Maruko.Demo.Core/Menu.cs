using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Domain.Entities.Auditing;

namespace Maruko.Demo.Core
{
    /// <summary>
    /// 菜单表
    /// </summary>
    public class Menu : FullAuditedEntity
    {
        public long ParentId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }
    }
}
