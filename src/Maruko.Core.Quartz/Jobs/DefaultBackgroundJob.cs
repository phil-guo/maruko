using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace Maruko.Core.Quartz.Jobs
{
    [PersistJobDataAfterExecution, DisallowConcurrentExecution]
    public class DefaultBackgroundJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            //throw new NotImplementedException();

            return Task.CompletedTask;
        }
    }
}
