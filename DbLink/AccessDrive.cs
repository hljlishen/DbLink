using System.Data.Common;
using System.Data.OleDb;

namespace DbLink
{
    internal class AccessDrive : DatabaseDrive
    {
        private static AccessDrive _instance;
        private static readonly object SyncRoot = new object();
        private AccessDrive(string connString)
        {
            DatabaseConnection = new OleDbConnection(connString);
            OpenDatabaseConnection();
        }

        protected override DbDataAdapter CreateAdapter(DbCommand cmd) => new OleDbDataAdapter((OleDbCommand)cmd);

        public static AccessDrive GetInstance(string connStr)
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance != null) return _instance;
                    _instance = new AccessDrive(connStr);
                }

                return _instance;
            }

            return _instance;
        }
    }
}