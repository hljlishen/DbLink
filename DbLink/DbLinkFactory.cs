namespace DbLink
{
    public abstract class DbLinkFactory
    {
        protected string ConnectString;

        protected DbLinkFactory(string connectString)
        {
            ConnectString = connectString;
        }

        public ISelectSqlMaker CreateSelectSqlMaker(string tableName) => new SelectSqlMaker(tableName);

        public abstract IDatabaseDrive CreateDatabaseDrive();

        public abstract IDateTimeFormater CreateDateTimeFormater();
    }

    internal class MySqlFactory : DbLinkFactory
    {
        public MySqlFactory(string connectString) : base(connectString)
        {
        }

        public override IDateTimeFormater CreateDateTimeFormater() => new StanderdStyleDateTimeFormater();

        public override IDatabaseDrive CreateDatabaseDrive() => MySqlDrive.GetInstance(ConnectString);
    }

    internal class AccessFactory : DbLinkFactory
    {
        public AccessFactory(string connectString) : base(connectString)
        {
        }

        public override IDateTimeFormater CreateDateTimeFormater() => new AccessStyleDateTimeFormater();

        public override IDatabaseDrive CreateDatabaseDrive() => AccessDrive.GetInstance(ConnectString);
    }

    internal class SqlServerFactory : DbLinkFactory
    {
        public SqlServerFactory(string connectString) : base(connectString)
        {
        }

        public override IDatabaseDrive CreateDatabaseDrive() => SqlServerDrive.GetInstance(ConnectString);

        public override IDateTimeFormater CreateDateTimeFormater() => new StanderdStyleDateTimeFormater();
    }
}
