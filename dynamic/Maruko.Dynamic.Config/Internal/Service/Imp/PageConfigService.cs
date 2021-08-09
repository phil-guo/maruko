using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;

namespace Maruko.Dynamic.Config
{
    public class PageConfigService : CurdAppService<PageConfig, PageConfigDTO>, IPageConfigService
    {
        public PageConfigService(IObjectMapper objectMapper, IFreeSqlRepository<PageConfig> repository) : base(objectMapper, repository)
        {
        }
    }
}
