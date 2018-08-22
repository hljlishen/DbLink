using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DbLink
{
    internal class MySqlDrive : DatabaseDrive
    {
        private static MySqlDrive _instance;
        private static readonly object SyncRoot = new object();
        private MySqlDrive(string connString)
        {
            DatabaseConnection = new MySqlConnection(connString);
            DatabaseConnection.Open();
        }
        protected override DbDataAdapter CreateAdapter(DbCommand cmd) => new MySqlDataAdapter((MySqlCommand)cmd);

        public static MySqlDrive GetInstance(string connStr)
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new MySqlDrive(connStr);
                    }
                }
            }
            return _instance;
        }
    }
}