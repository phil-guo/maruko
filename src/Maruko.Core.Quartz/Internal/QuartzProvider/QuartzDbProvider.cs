using Maruko.Core.Config;
using Microsoft.Extensions.Configuration;

namespace Maruko.Core.Quartz.Internal.QuartzProvider
{
    public class QuartzDbProvider : IQuartzDbProvider
    {
        private IConfiguration _configuration;

        public QuartzDbProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Connection
        {
            get
            {
                var coreAppConfig = new AppConfig(_configuration);
                return coreAppConfig.Core.Connection;
            }
        }
    }
}
