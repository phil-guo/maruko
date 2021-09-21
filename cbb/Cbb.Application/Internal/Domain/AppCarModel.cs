using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Cbb.Application
{
    [Table(Name = "dc_car_model")]
    public class AppCarModel : FreeSqlEntity
    {
        [Column(Name = "ver")]
        public int Ver { get; set; }
        [Column(Name = "car_model_id")]
        public long CarModelId { get; set; }
        [Column(Name = "vehicle_system_id")]
        public long VehicleSystemId { get; set; }
        [Column(Name = "brand_id")]
        public long BrandId { get; set; }
        [Column(Name = "car_model_name", StringLength = 32)]
        public string Name { get; set; }
        [Column(Name = "price", DbType = "decimal(20,2)")]
        public decimal Price { get; set; }
    }
}
