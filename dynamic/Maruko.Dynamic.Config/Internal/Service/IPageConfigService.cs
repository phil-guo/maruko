﻿using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.FreeSql.Internal.AppService;

namespace Maruko.Dynamic.Config
{
    public interface IPageConfigService : ICurdAppService<PageConfig, PageConfigDTO>
    {
    }
}
