using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Cbb.Application
{
    [Table(Name = "app_car_brand")]
    public class AppCarBrand : FreeSqlEntity
    {
        [Column(Name = "ver")]
        public int Ver { get; set; }

        [Column(Name = "brand_id")]
        public long BrandId { get; set; }

        [Column(Name = "name", StringLength = 32)]
        public string Name { get; set; }

        [Column(Name = "first_letter", StringLength = 1)]
        public string FirstLetter { get; set; }
    }
}
