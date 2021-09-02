using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maruko.Core.Application;
using Quartz;

namespace Maruko.TaskScheduling
{
    public interface IStrategy
    {
        Task<AjaxResponse<object>> ExecuteAsync(ExecuteRequest request);
    }
}
