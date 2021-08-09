using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;

namespace Maruko.Dynamic.Config
{
    public class PageService : CurdAppService<Page, PageDTO>, IPageService
    {
        public PageService(IObjectMapper objectMapper, IFreeSqlRepository<Page> repository) : base(objectMapper, repository)
        {
        }
    }
}
