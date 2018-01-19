using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Maruko.SqlSugarCore.UnitOfWork
{
    public class SugarUnitOfwork : InstallSqlSugar, ISugarUnitOfWork
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void CommitAndRefreshChanges()
        {
            throw new NotImplementedException();
        }

        public void RollbackChanges()
        {
            throw new NotImplementedException();
        }

        public SqlSugarClient SqlSugarClient(bool isEnableLog = false)
        {
            return Instence(isEnableLog);
        }
    }
}
