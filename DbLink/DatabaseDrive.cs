using System;
using System.Data;
using System.Data.Common;

namespace DbLink
{
    public abstract class DatabaseDrive : IDatabaseDrive, IDisposable 
    {
        protected DbConnection DatabaseConnection;        //数据库连接对象

        public void CloseDatabaseConnection() => DatabaseConnection.Close();

        public void OpenDatabaseConnection() => DatabaseConnection.Open();  //子类的构造函数最后都应该调用该函数

        private DataSet ExecuteQuery(string querySql)
        {
            if(!IsConnectionOpen())
                throw new Exception("ExecuteQuery()执行时，DatabaseConnection的状态不是Open");
            DataSet queryResult = new DataSet();
            DbCommand dbCmd = CreateCommand(querySql);
            DbDataAdapter adapter = CreateAdapter(dbCmd);
            adapter.Fill(queryResult);
            return queryResult;
        }

        private void ExecuteNonQuery(string nonQuerySql)
        {
            if(!IsConnectionOpen())
                throw new Exception(@"ExecuteNonQuery()执行时，DatabaseConnection的状态不是Open");
            DbCommand dbCmd = CreateCommand(nonQuerySql);
            var ret = dbCmd.ExecuteNonQuery();
            if(ret <= 0)
                throw new Exception($"ExecuteNonQuery()的影响行数为{ret}\r\n 输入参数为：{nonQuerySql}");
        }

        private bool IsConnectionOpen() => DatabaseConnection.State == ConnectionState.Open;

        protected virtual DbCommand CreateCommand(string sqlCmd)
        {
            DbCommand command = DatabaseConnection.CreateCommand();
            command.CommandText = sqlCmd;
            return command;
        }

        protected abstract DbDataAdapter CreateAdapter(DbCommand cmd);

        public void ExecuteInsert(string sql) => ExecuteNonQuery(sql);

        public void ExecuteUpdate(string sql) => ExecuteNonQuery(sql);

        public void ExecuteDelete(string sql) => ExecuteNonQuery(sql);

        public DataSet ExecuteSelect(string selectSql) => ExecuteQuery(selectSql);

        public void Dispose()
        {
            CloseDatabaseConnection();
            DatabaseConnection.Dispose();
        }
    }
}
