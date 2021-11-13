using Autofac;
using Maruko.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Cbb.Application
{
    public class AppConfig
    {
        public static AppConfig Default = new AppConfig();
        public CbbOption Cbb { get; set; }
        private IConfiguration configuration => ServiceLocator.Current.Resolve<IConfiguration>();

        public AppConfig()
        {
            var section = configuration?.GetSection(nameof(Cbb));
            Cbb = section.Exists()
                ? section.Get<CbbOption>()
                : new CbbOption();
        }
    }
}