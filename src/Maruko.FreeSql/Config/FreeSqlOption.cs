using FreeSql;

namespace Maruko.Core.FreeSql.Config
{
    public class FreeSqlOption
    {
        public string Connection { get; set; }

        public DataType DbType { get; set; } = DataType.MySql;

        public bool EnableDatabaseMigrate { get; set; } = false;
        public bool EnableSeedData { get; set; } = false;
    }
}
