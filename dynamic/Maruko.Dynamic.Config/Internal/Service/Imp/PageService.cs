using System;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;

namespace Maruko.Dynamic.Config
{
    public class PageService : CurdAppService<Page, PageDTO>, IPageService
    {
        public PageService(IObjectMapper objectMapper, IFreeSqlRepository<Page> repository) : base(objectMapper,
            repository)
        {
        }

        protected override void BeforeCreate(PageDTO request)
        {
            Validate(request);
        }

        protected override void BeforeEdit(PageDTO request)
        {
            Validate(request);
        }

        private void Validate(PageDTO request)
        {
            if (string.IsNullOrEmpty(request.Key))
                throw new Exception("页面标识不能为空");
            if (string.IsNullOrEmpty(request.Name))
                throw new Exception("页面名称不能为空");

            var page = FirstOrDefault(item => item.Key == request.Key);
            if (page != null && request.Id == 0)
                throw new Exception($"已经存在key:{request.Key},请重新填写");
        }
    }
}