using System;
using Maruko.Dependency.Installers;
using Maruko.Domain.Entities.Auditing;
using Maruko.SqlSugarCore.Reponsitory;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Xunit;

namespace Maruko.SqlSugar.Test
{
    public class UnitTest1
    {
        private readonly IMarukoSugarRepository<Test, long> _sugarRepository;

        public UnitTest1()
        {
            IServiceCollection services = new ServiceCollection();
            //×¢Èë
            services.AddDependencyRegister();
            //¹¹½¨ÈÝÆ÷
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _sugarRepository = serviceProvider.GetService<IMarukoSugarRepository<Test, long>>();
        }

        [Fact]
        public void Test1()
        {
            var test1 = _sugarRepository.SugarUnitOfWork.SqlSugarClient().Queryable<Test>().Single(item => item.Id == 3);
            var test2 = _sugarRepository.FirstOrDefault(item => item.Id == 3);

            Assert.NotNull(test1);
            Assert.NotNull(test2);
        }
    }
    [SugarTable("ty_account_memberinfo")]
    public class Test : FullAuditedEntity<long>
    {
        [SugarColumn(IsIgnore = true)]
        public override bool IsDeleted { get; set; }

        [SugarColumn(IsIgnore = true)]
        public override DateTime? LastModificationTime { get; set; }
    }
}
