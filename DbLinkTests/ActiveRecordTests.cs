using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLink.Tests
{
    [TestClass()]
    public class ActiveRecordTests
    {
        private User _user;

        [TestMethod()]
        public void InsertTest()
        {
            Setup();
            TestAllConditionNotNull();
            TestLastConditionNullAndDateNull();
            TestConditionFirstNullAndStringNull();
            TestConditionSecondNull();
            TestCondition3ThNullAndIntNull();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Setup();
            _user.Name = "李四";
            _user.Department = "JDR";
            _user.Number = null;
            _user.Id = 1;
            string expected = "update User set Id=1,Name='李四',Department='JDR' where Id=1";
            string actual = _user.MakeUpdateSqlCommand();

            Assert.AreEqual(expected, actual);
        }

        private void Setup()
        {
            DbLinkFactory factory = DbLink.CreateFactory(DataBaseType.MySql);
            IDatabaseDrive mySqlDrive = factory.CreateDatabaseDrive(@"Server=localhost;Database=activerecordtest;user id= root;password= root;");
            IDateTimeFormater formater = factory.CreateDateTimeFormater();
            _user = new User(mySqlDrive, formater);
        }

        private void TestAllConditionNotNull()
        {
            _user.Name = "张三";
            _user.Department = "JDR";
            _user.Number = 123;
            _user.InsertTime = new DateTime(2018, 3, 11, 23, 14, 59);

            string expected = "insert into User (Name,Department,Number,InsertTime) values ('张三','JDR',123,'2018-03-11 23:14:59')";
            string actual = _user.MakeInsertSqlCommand();

            Assert.AreEqual(expected, actual);
        }

        private void TestLastConditionNullAndDateNull()
        {
            _user.Name = "张三";
            _user.Department = "JDR";
            _user.Number = 123;
            _user.InsertTime = null;

            string expected = "insert into User (Name,Department,Number) values ('张三','JDR',123)";
            string actual = _user.MakeInsertSqlCommand();

            Assert.AreEqual(expected, actual);
        }

        private void TestConditionSecondNull()
        {
            _user.Name = "张三";
            _user.Department = null;
            _user.Number = 123;
            _user.InsertTime = new DateTime(2018, 3, 11, 23, 14, 59);

            string expected = "insert into User (Name,Number,InsertTime) values ('张三',123,'2018-03-11 23:14:59')";
            string actual = _user.MakeInsertSqlCommand();

            Assert.AreEqual(expected, actual);
        }

        private void TestConditionFirstNullAndStringNull()
        {
            _user.Name = null;
            _user.Department = "JDR";
            _user.Number = 123;
            _user.InsertTime = new DateTime(2018, 3, 11, 23, 14, 59);

            string expected = "insert into User (Department,Number,InsertTime) values ('JDR',123,'2018-03-11 23:14:59')";
            string actual = _user.MakeInsertSqlCommand();

            Assert.AreEqual(expected, actual);
        }

        private void TestCondition3ThNullAndIntNull()
        {
            _user.Name = "张三";
            _user.Department = "JDR";
            _user.Number = null;
            _user.InsertTime = new DateTime(2018, 3, 11, 23, 14, 59);

            string expected = "insert into User (Name,Department,InsertTime) values ('张三','JDR','2018-03-11 23:14:59')";
            string actual = _user.MakeInsertSqlCommand();

            Assert.AreEqual(expected, actual);
        }
    }
}