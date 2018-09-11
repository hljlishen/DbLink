using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DbLink
{
    class SqlServerDrive : DatabaseDrive
    {
        private static SqlServerDrive _instance;
        private static readonly object SyncRoot = new object();

        public static SqlServerDrive GetInstance(string connStr)
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new SqlServerDrive(connStr);
                    }
                }
            }
            return _instance;
        }

        private SqlServerDrive(string connString)
        {
            DatabaseConnection = new SqlConnection(connString);
            DatabaseConnection.Open();
        }
        protected override DbDataAdapter CreateAdapter(DbCommand cmd) => new SqlDataAdapter((SqlCommand)cmd);
    }
}
