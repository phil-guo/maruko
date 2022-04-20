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
        private readonly IFreeSqlRepository<Page> _page;
        public PageConfigService(IObjectMapper objectMapper, IFreeSqlRepository<PageConfig> repository, IFreeSqlRepository<Page> page) : base(objectMapper, repository)
        {
            _page = page;
        }

        public override PageConfigDTO CreateOrEdit(PageConfigDTO request)
        {
            PageConfig data = null;

            var page = _page.FirstOrDefault(item => item.Key == request.Key);

            if (page == null)
                return new PageConfigDTO();

            request.PageId = request.PageId = page.Id;
            if (request.Id == 0)
            {
                BeforeCreate(request);
                request.CreateTime = DateTime.Now;
                data = Table.Insert(MapToEntity(request));
            }
            else
            {
                BeforeEdit(request);
                data = Table.FirstOrDefault(item => item.Id == request.Id);
                data = MapToEntity(request);
                data = Table.Update(data);
            }

            return data == null
                ? null
                : ObjectMapper.Map<PageConfigDTO>(data);
        }
    }
}
