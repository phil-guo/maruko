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
        private readonly IFreeSqlRepository<PageTableConfig> _pageTable;
        private readonly IObjectMapper _objectMapper;

        public PageDomainService(IFreeSqlRepository<Page> page,
            IFreeSqlRepository<PageTableConfig> pageTable,
            IObjectMapper objectMapper)
        {
            _page = page;
            _pageTable = pageTable;
            _objectMapper = objectMapper;
        }

        public GetPageDetailDTO GetPageDetail(string key)
        {
            var page = _page.FirstOrDefault(item => item.Key == key);
            if (page == null)
                throw new Exception("page 不存在");

            var pageTable = _pageTable.FirstOrDefault(item => item.PageId == page.Id);

            return new GetPageDetailDTO()
            {
                PageTable = _objectMapper.Map<PageTableConfig>(pageTable)
            };

        }
    }
}
