using System;

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
        public abstract SelectSqlMaker CreateSelectSqlMaker(string fieldName);

        public abstract IDatabaseDrive CreateDatabaseDrive(string connectString);

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

        public override IDatabaseDrive CreateDatabaseDrive(string connectString) => MySqlDrive.GetInstance(connectString);

        protected override TableField MakeSpecificDateTimeField(string fieldName)
        {
            return new DateTimeField(fieldName, null, new StanderdStyleDateTimeFormater());
        }

        protected override TableField MakeSpecificDateField(string fieldName)
        {
            return new DateField(fieldName, null, new StanderdStyleDateTimeFormater());
        }
    }

    internal class AccessFactory : DbLinkFactory
    {
        public override SelectSqlMaker CreateSelectSqlMaker(string tableName)
        {
            return new SelectSqlMaker(tableName);
        }
        public override IDateTimeFormater CreateDateTimeFormater() => new AccessStyleDateTimeFormater();

        public override IDatabaseDrive CreateDatabaseDrive(string connectString) => AccessDrive.GetInstance(connectString);

        protected override TableField MakeSpecificDateTimeField(string fieldName)
        {
            return new DateTimeField(fieldName, null ,new AccessStyleDateTimeFormater());
        }

        protected override TableField MakeSpecificDateField(string fieldName)
        {
            return new DateField(fieldName, null, new AccessStyleDateTimeFormater());
        }
    }
}
