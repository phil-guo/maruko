using System;
using System.Collections.Generic;
using System.Text;
using Maruko.SqlSugarCore.UnitOfWork;

namespace Maruko.SqlSugar.Test
{
    public class TestUnitOfWork: SugarUnitOfwork
    {
        public override string ConnectionString { get; set; } = "server=192.168.2.110;uid=tykj;pwd=taoyikeji520!;database=CDPC_DB";
    }
}
