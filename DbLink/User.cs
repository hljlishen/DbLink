using System;

namespace DbLink
{
    public class User : ActiveRecord
    {
        public User(DbLinkFactory factory) : base("User","Id", factory)
        {
        }

        //private int _thisIsNotProperty = 13;

        //public int Get() => _thisIsNotProperty;
        //public int Set(int value) => _thisIsNotProperty = value;

        //public int? ThisIsProperty { get; set; } = 14;
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