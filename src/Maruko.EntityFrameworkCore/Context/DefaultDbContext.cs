using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.EntityFrameworkCore.Context
{
    public class DefaultDbContext : BaseDbContext
    {
        public DefaultDbContext(string connStr) 
            : base(connStr)
        {
        }
    }
}
