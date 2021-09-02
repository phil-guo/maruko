using System;
using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.TaskScheduling
{
    [Table(Name = "app_all_country_oil_price")]
    public class AllCountryOilPrice : FreeSqlEntity
    {
        [Column(Name = "city_name", StringLength = 12)]
        public string CityName { get; set; }

        [Column(Name = "price_json", StringLength = 256)]
        public string PriceJson { get; set; }

        [Column(Name = "notify", StringLength = 56)]
        public string NextNotify { get; set; }
    }
}