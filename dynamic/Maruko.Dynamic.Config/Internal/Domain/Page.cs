using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.Dynamic.Config
{
    [Table(Name = "dc_page")]
    public class Page : FreeSqlEntity
    {
        [Column(Name = "name", StringLength = 100)]
        public string Name { get; set; }
        [Column(Name = "key", StringLength = 200)]
        public string Key { get; set; }
    }
}
