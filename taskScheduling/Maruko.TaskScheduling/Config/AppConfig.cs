using Autofac;
using Maruko.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Maruko.TaskScheduling
{
    public class AppConfig
    {
        public static AppConfig Default = new AppConfig();
        public ScheduleOption Schedule { get; set; }
        private IConfiguration configuration => ServiceLocator.Current.Resolve<IConfiguration>();
        
        public AppConfig()
        {
            var section = configuration?.GetSection(nameof(Schedule));
            Schedule = section.Exists()
                ? section.Get<ScheduleOption>()
                : new ScheduleOption();
        }
    }
}