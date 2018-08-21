using System;
using System.Reflection;

namespace DbLink
{
    public enum TableFieldType
    {
        Int,
        Double,
        String,
        Date,
        DateTime
    }
    public abstract class DbLinkFactory
    {
        protected string ConnectString;

        protected DbLinkFactory(string connectString)
        {
            ConnectString = connectString;
        }
        public abstract SelectSqlMaker CreateSelectSqlMaker(string fieldName);

        public abstract IDatabaseDrive CreateDatabaseDrive();

        public abstract IDateTimeFormater CreateDateTimeFormater();

        public TableField CreateTableField(string fieldName, TableFieldType type)
        {
            switch (type)
            {
                case TableFieldType.Int:
                    return new IntField(fieldName, null);
                case TableFieldType.Double:
                    return new DoubleField(fieldName, null);
                case TableFieldType.String:
                    return new StringField(fieldName, null);
                case TableFieldType.DateTime:
                    return MakeSpecificDateTimeField(fieldName);
                case TableFieldType.Date:
                    return MakeSpecificDateField(fieldName);
                default:
                    throw new Exception("错误的类型");
            }
        }

        protected abstract TableField MakeSpecificDateTimeField(string fieldName);

        protected abstract TableField MakeSpecificDateField(string fieldName);
    }

    internal class MySqlFactory : DbLinkFactory
    {
        public override SelectSqlMaker CreateSelectSqlMaker(string tableName)
        {
            return new SelectSqlMaker(tableName);
        }
        public override IDateTimeFormater CreateDateTimeFormater() => new StanderdStyleDateTimeFormater();

        public override IDatabaseDrive CreateDatabaseDrive() => MySqlDrive.GetInstance(ConnectString);

        protected override TableField MakeSpecificDateTimeField(string fieldName)
        {
            return new DateTimeField(fieldName, null, new StanderdStyleDateTimeFormater());
        }

        protected override TableField MakeSpecificDateField(string fieldName)
        {
            return new DateField(fieldName, null, new StanderdStyleDateTimeFormater());
        }

        public MySqlFactory(string connectString) : base(connectString)
        {
        }
    }

    internal class AccessFactory : DbLinkFactory
    {
        public override SelectSqlMaker CreateSelectSqlMaker(string tableName)
        {
            return new SelectSqlMaker(tableName);
        }
        public override IDateTimeFormater CreateDateTimeFormater() => new AccessStyleDateTimeFormater();

        public override IDatabaseDrive CreateDatabaseDrive() => AccessDrive.GetInstance(ConnectString);

        protected override TableField MakeSpecificDateTimeField(string fieldName)
        {
            return new DateTimeField(fieldName, null ,new AccessStyleDateTimeFormater());
        }

        protected override TableField MakeSpecificDateField(string fieldName)
        {
            return new DateField(fieldName, null, new AccessStyleDateTimeFormater());
        }

        public AccessFactory(string connectString) : base(connectString)
        {
        }
    }
}
