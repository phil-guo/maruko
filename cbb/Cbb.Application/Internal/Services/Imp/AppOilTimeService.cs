using System;
using Cbb.Application.Internal.Domain;
using Cbb.Application.Internal.Services.DTO;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;

namespace Cbb.Application.Internal.Services.Imp
{
    public class AppOilTimeService : CurdAppService<AppOilTime, AppOilTimeDTO>, IAppOilTimeService
    {
        public AppOilTimeService(IObjectMapper objectMapper, IFreeSqlRepository<AppOilTime> repository) : base(objectMapper, repository)
        {
        }

        protected override void BeforeCreate(AppOilTimeDTO request)
        {
            request.Year = DateTime.Now.Year;
            var entity = Table
                .GetAll()
                .Select<AppOilTime>()
                .OrderByDescending(item => item.CreateTime)
                .ToOne();
            if (entity == null)
                return;

            request.Sort = entity.Sort + 1;
        }

        protected override void BeforeEdit(AppOilTimeDTO request)
        {
            var entity = FirstOrDefault(request.Id);
            request.Sort = entity.Sort;
            request.Year=entity.Year;
        }
    }
}
