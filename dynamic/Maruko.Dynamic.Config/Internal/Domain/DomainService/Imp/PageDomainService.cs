using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;

namespace Maruko.Dynamic.Config.Internal.Domain.DomainService.Imp
{
    public class PageDomainService : IPageDomainService
    {
        private readonly IFreeSqlRepository<Page> _page;
        private readonly IFreeSqlRepository<PageConfig> _pageConfig;
        private readonly IObjectMapper _objectMapper;

        public PageDomainService(IFreeSqlRepository<Page> page,
            IFreeSqlRepository<PageConfig> pageTable,
            IObjectMapper objectMapper)
        {
            _page = page;
            _pageConfig = pageTable;
            _objectMapper = objectMapper;
        }

        public GetPageDetailDTO GetPageDetail(string key)
        {
            var page = _page.FirstOrDefault(item => item.Key == key);
            if (page == null)
                throw new Exception("page 不存在");

            var pageConfig = _pageConfig.FirstOrDefault(item => item.PageId == page.Id);

            var pageConfigDTO = _objectMapper.Map<PageConfigDTO>(pageConfig);
            if (pageConfigDTO != null)
                pageConfigDTO.Key = page.Key;

            return new GetPageDetailDTO()
            {
                PageConfigs = pageConfigDTO ?? new PageConfigDTO()
            };
        }
    }
}