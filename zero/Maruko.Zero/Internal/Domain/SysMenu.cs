using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using Maruko.Core.FreeSql.Internal;

namespace Maruko.Zero
{
    [Table(Name = "sys_menu")]
    public class SysMenu : FreeSqlEntity
    {
        [Column(Name = "parentId")]
        public int ParentId { get; set; } = 0;

        [Column(Name = "name", StringLength = 20)]
        public string Name { get; set; }

        [Column(Name = "url", StringLength = 100)]
        public string Url { get; set; }


        [Column(DbType = "tinyint(4)", Name = "level")]
        public int Level { get; set; } = 1;


        [Column(Name = "operates", StringLength = 100)]
        public string Operates { get; set; }

        public int Sort { get; set; }

        [Column(Name = "icon", StringLength = 100)]
        public string Icon { get; set; }

        [Column(Name = "key", StringLength = 50)]
        public string Key { get; set; }

        public int AddOperateSort()
        {
            Sort += 1;
            return Sort;
        }
    }
}