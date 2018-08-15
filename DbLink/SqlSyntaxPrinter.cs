using System;
using System.Data;

namespace DbLink
{
    class SqlSyntaxPrinter : IDatabaseDrive      //测试用，只是打印sql语句，用于检查语法是否正确
    {
        public void ExecuteInsert(string sql) => PrintSql(sql);

        public void ExecuteUpdate(string sql) => PrintSql(sql);

        public void ExecuteDelete(string sql) => PrintSql(sql);

        public DataSet ExecuteSelect(string selectSql)
        {
            PrintSql(selectSql);
            return new DataSet();
        }

        private static void PrintSql(string sql) => Console.WriteLine("{" + sql + "} executed!");
    }
}