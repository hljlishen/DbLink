using System;

namespace DbLink
{
    public class User : ActiveRecord
    {
        public User(DbLinkFactory factory) : base("User","Id", factory)
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

        public double? Doubletest
        {
            get;
            set;
        }
    }
}