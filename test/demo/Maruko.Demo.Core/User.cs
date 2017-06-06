using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Domain.Entities.Auditing;

namespace Maruko.Demo.Core
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User : FullAuditedEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
