using System;

namespace DbLink
{
    public class User : ActiveRecord
    {
        public User(IDatabaseDrive dbDrive, IDateTimeFormater dateTimeFormater) : base("User","Id", dbDrive, dateTimeFormater)
        {
        }

        public int? Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        public string Department
        {
            get;
            set;
        }

        public int? Number
        {
            get;
            set;
        }

        public DateTime? InsertTime
        {
            get;
            set;
        }

        public double? DoubleTest
        {
            get;
            set;
        }
    }
}