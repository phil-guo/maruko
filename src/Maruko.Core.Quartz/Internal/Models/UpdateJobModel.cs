namespace Maruko.Core.Quartz.Internal.Models
{
    public class UpdateJobModel
    {
        public ScheduleModel NewModel { get; set; }
        public ScheduleModel OldModel { get; set; }
    }
}
