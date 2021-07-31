using FreeSql.DataAnnotations;
using Maruko.Core.Domain.Entities;

namespace Maruko.Core.Test.FreeSql.Model
{
    [Table(Name = "vehicle")]
    public class Vehicle : Entity
    {
        [Column(Name = "vehicle_number", StringLength = 32)]
        public string Number { get; set; }

        [Column(Name = "Load_capacity")]
        public string LoadCapacity { get; set; }

        /// <summary>
        /// 0.可用 1.不可用(运送中)
        /// </summary>
        [Column(Name = "current_state")]
        public string CurrentState { get; set; }

        /// <summary>
        /// 0.小型1.中型2.大型
        /// </summary>
        [Column(Name = "type")]
        public string Type { get; set; }

        public bool IsComplete { get; set; }
    }
}
