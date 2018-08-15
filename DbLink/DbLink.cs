using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLink
{
    public enum DataBaseType
    {
        MySql,
        Access
    }
    public class DbLink
    {
        public static DbLinkFactory CreateFactory(DataBaseType type)
        {
            switch (type)
            {
                case DataBaseType.MySql:
                    return new MySqlFactory();
                case DataBaseType.Access:
                    return new AccessFactory();
                default:
                    throw new Exception("错误的数据库类型");
            }
        }
    }
}
