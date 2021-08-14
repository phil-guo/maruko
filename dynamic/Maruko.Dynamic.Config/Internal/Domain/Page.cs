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
        // [Column(Name = "url", StringLength = 200)]
        // public string DataUrl { get; set; }
        // [Column(Name = "isRow")]
        // public bool IsRow { get; set; }
        // [Column(Name = "isMultiple")]
        // public bool IsMultiple { get; set; }
        // [Column(Name = "rowWith")]
        // public int RowWith { get; set; }
        // [Column(Name = "defaultQueryCondition")]
        // public string DefaultQueryCondition { get; set; }
    }
}
