using Autofac;
using Maruko.Core.Extensions;
using Maruko.Dynamic.Config;
using Xunit;
using Xunit.Abstractions;

namespace Maruko.Core.Test.Dynamic.Config
{
    public class PageTest : TestMarukoCoreBase
    {
        private readonly IPageService _page;

        public PageTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _page = ServiceLocator.Current.Resolve<IPageService>();
        }

        [Fact]
        public void Create_Test()
        {
            var one = _page.CreateOrEdit(new PageDTO()
            {
                Name = "数据字典",
                Key = "dataDictionary"
            });
            
            Print(one);
        }
    }
}