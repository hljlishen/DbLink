using System;

namespace DbLink
{
    public enum DatabaseType
    {
        MySql,
        Access,
        SqlServer
    }
    public class DbLinkGateway
    {
        public static DbLinkFactory CreateFactory(DatabaseType type)
        {
            switch (type)
            {
                case DatabaseType.MySql:
                    return new MySqlFactory();
                case DatabaseType.Access:
                    return new AccessFactory();
                case DatabaseType.SqlServer:
                    return new SqlServerFactory();
                default:
                    throw new Exception("错误的数据库类型");
            }
        }
    }
}
