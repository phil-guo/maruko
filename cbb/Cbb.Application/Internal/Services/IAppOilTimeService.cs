using System;
using System.Collections.Generic;
using System.Text;
using Cbb.Application.Internal.Domain;
using Cbb.Application.Internal.Services.DTO;
using Maruko.Core.FreeSql.Internal.AppService;

namespace Cbb.Application.Internal.Services
{
    public interface IAppOilTimeService : ICurdAppService<AppOilTime, AppOilTimeDTO>
    {
    }
}
