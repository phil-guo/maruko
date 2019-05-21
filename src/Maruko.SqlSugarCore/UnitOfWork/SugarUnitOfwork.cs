using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Maruko.SqlSugarCore.UnitOfWork
{
    public class SugarUnitOfwork : InstallSqlSugar, ISugarUnitOfWork
    {
        public int Commit()
        {
            throw new NotImplementedException();
        }

        public void CommitAndRefreshChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (SqlSugarClient() != null)
                SqlSugarClient().Dispose();
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
