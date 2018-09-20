namespace DbLink
{
    public abstract class DbLinkFactory
    {
        public abstract IDatabaseDrive CreateDatabaseDrive(string connectString);

        public abstract IDateTimeFormater CreateDateTimeFormater();

        public abstract DatabaseType GetDataBaseType();
    }

    internal class MySqlFactory : DbLinkFactory
    {

        public override IDateTimeFormater CreateDateTimeFormater() => new StanderdStyleDateTimeFormater();
        public override DatabaseType GetDataBaseType() => DatabaseType.MySql;

        public override IDatabaseDrive CreateDatabaseDrive(string connectString) => MySqlDrive.GetInstance(connectString);
    }

    internal class AccessFactory : DbLinkFactory
    {
        public override IDateTimeFormater CreateDateTimeFormater() => new AccessStyleDateTimeFormater();
        public override DatabaseType GetDataBaseType() => DatabaseType.Access;

        public override IDatabaseDrive CreateDatabaseDrive(string connectString) => AccessDrive.GetInstance(connectString);
    }

    internal class SqlServerFactory : DbLinkFactory
    {

        public override IDatabaseDrive CreateDatabaseDrive(string connectString) => SqlServerDrive.GetInstance(connectString);

        public override IDateTimeFormater CreateDateTimeFormater() => new StanderdStyleDateTimeFormater();
        public override DatabaseType GetDataBaseType() => DatabaseType.SqlServer;
    }
}
