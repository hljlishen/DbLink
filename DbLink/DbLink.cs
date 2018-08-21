using System;

namespace DbLink
{
    public enum DataBaseType
    {
        MySql,
        Access
    }
    public class DbLink
    {
        public static DbLinkFactory CreateFactory(DataBaseType type, string databaseConnectString)
        {
            switch (type)
            {
                case DataBaseType.MySql:
                    return new MySqlFactory(databaseConnectString);
                case DataBaseType.Access:
                    return new AccessFactory(databaseConnectString);
                default:
                    throw new Exception("错误的数据库类型");
            }
        }
    }
}
