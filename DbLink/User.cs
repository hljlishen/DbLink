using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DbLink
{
    public class User : ActiveRecord
    {
        public User(IDatabaseDrive dbDrive, IDateTimeFormater dateTimeFormater) : base("User","Id", dbDrive, dateTimeFormater)
        {
        }

        public int? Id
        {
            get => (int?)GetFieldValue("Id");
            set => SetFieldValue("Id", value);
        }
        public string Name
        {
            get => (string)GetFieldValue("Name");
            set => SetFieldValue("Name", value);
        }

        public string Department
        {
            get => (string)GetFieldValue("Department");
            set => SetFieldValue("Department", value);
        }

        public int? Number
        {
            get => (int?)GetFieldValue("Number");
            set => SetFieldValue("Number", value);
        }

        public DateTime? InsertTime
        {
            get => (DateTime?)GetFieldValue("InsertTime");
            set => SetFieldValue("InsertTime", value);
        }

        public double? DoubleTest
        {
            get => (double?)GetFieldValue("DoubleTest");
            set => SetFieldValue("DoubleTest", value);
        }
    }
}