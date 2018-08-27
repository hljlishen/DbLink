using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DbLink.Tests
{
    [TestClass()]
    public class SelectSqlMakerTests
    {
        private SelectSqlMaker _maker;
        private DbLinkFactory _factory;

        private void Setup()
        {
            _factory = DbLink.CreateFactory(DataBaseType.MySql, @"Server=localhost;Database=activerecordtest;user id= root;password= root;");
            _maker = new SelectSqlMaker("User");
        }

        [TestMethod()]
        public void TestNoCondition()
        {
            Setup();
            string expected = "select * from User";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestSingleFieldSingleCondition()
        {
            Setup();
            _maker.AddAndCondition(new StringEqual("Name", "张三"));
            _maker.AddSelectField("Name");

            string expected = "select Name from User where Name='张三'";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestDoubleFieldsDoubleConditions()
        {
            Setup();
            _maker.AddAndCondition(new StringEqual("Name", "张三"));
            _maker.AddAndCondition(new IntEqual("Id", 1));
            _maker.AddSelectField("Name");
            _maker.AddSelectField("Department");

            string expected = "select Name,Department from User where Name='张三' and Id=1";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestIntBetweenCondition()
        {
            Setup();
            _maker.AddAndCondition(new IntBetweenCloseInterval("Id", 10, 1));
            string actual = _maker.MakeSelectSql();
            string expected = "select * from User where Id>=1 and Id<=10";
            Assert.AreEqual(expected, actual);

            Setup();
            _maker.AddAndCondition(new IntBetweenOpenInterval("Id", 10, 1));
            actual = _maker.MakeSelectSql();
            expected = "select * from User where Id>1 and Id<10";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestDateEqual()
        {
            Setup();
            DateTime date = new DateTime(2018, 9, 5);
            _maker.AddAndCondition(new DateEqual("InsertTime", date, _factory.CreateDateTimeFormater()));
            _maker.AddAndCondition(new StringEqual("Name", "张三"));

            string expected = "select * from User where InsertTime='2018-09-05' and Name='张三'";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestSingleOrCondition()
        {
            Setup();
            DateTime date = new DateTime(2018, 9, 5);
            _maker.AddAndCondition(new DateEqual("InsertTime", date, _factory.CreateDateTimeFormater()));
            _maker.AddAndCondition(new StringEqual("Name", "张三"));
            _maker.AddOrCondition(new IntEqual("Number", 1));

            string expected = "select * from User where InsertTime='2018-09-05' and Name='张三' or Number=1";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestDoubleOrCondition()
        {
            Setup();
            DateTime date = new DateTime(2018, 9, 5);
            _maker.AddAndCondition(new DateEqual("InsertTime", date, _factory.CreateDateTimeFormater()));
            _maker.AddAndCondition(new StringEqual("Name", "张三"));
            _maker.AddOrCondition(new IntEqual("Number", 1));
            _maker.AddOrCondition(new DoubleBetween("DoubleTest", 15, 12));

            string expected = "select * from User where InsertTime='2018-09-05' and Name='张三' or Number=1 or DoubleTest>12 and DoubleTest<15";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestOrConditionOnly()
        {
            Setup();
            _maker.AddOrCondition(new IntEqual("Number", 1));
            _maker.AddOrCondition(new DoubleBetween("DoubleTest", 15, 12));

            string expected = "select * from User where Number=1 or DoubleTest>12 and DoubleTest<15";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestSingleOrConditionOnly()
        {
            Setup();
            _maker.AddOrCondition(new DoubleBetween("DoubleTest", 15, 12));

            string expected = "select * from User where DoubleTest>12 and DoubleTest<15";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void TestSingleNullCondition()
        {
            Setup();

            _maker.AddAndCondition(new IntEqual("Id", null));
            string expected = "select * from User";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestNullConditionAmongMultipleConditions()
        {
            Setup();
            _maker.AddAndCondition(new IntEqual("Id", 1));
            _maker.AddAndCondition(new StringEqual("Name", ""));
            _maker.AddAndCondition(new StringLike("Department", "软件"));

            string expected = "select * from User where Id=1 and Department like '%软件%'";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestAndConditionsWithSingleNullOrCondition()
        {
            Setup();
            _maker.AddAndCondition(new IntEqual("Id", 1));
            _maker.AddOrCondition(new StringEqual("Name", ""));
            _maker.AddAndCondition(new StringLike("Department", "软件"));

            string expected = "select * from User where Id=1 and Department like '%软件%'";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestSingleAndConditionDoubleOrConditionWithNull()
        {
            Setup();
            _maker.AddAndCondition(new IntEqual("Id", 1));
            _maker.AddOrCondition(new StringEqual("Name", ""));
            _maker.AddOrCondition(new StringLike("Department", "软件"));

            string expected = "select * from User where Id=1 or Department like '%软件%'";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestAllNullConditions()
        {
            Setup();
            _maker.AddAndCondition(new IntEqual("Id", null));
            _maker.AddOrCondition(new StringEqual("Name", ""));
            _maker.AddOrCondition(new StringLike("Department", null));
            _maker.AddAndCondition(new DoubleBetween("DoubleTest", null, null));

            string expected = "select * from User";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestDateBetweenOpenInterval()
        {
            Setup();
            DateTime begin = new DateTime(2018,9,9);
            DateTime end = new DateTime(2019,10,10);
            _maker.AddAndCondition(new DateBetweenOpenInterval("Date", begin, end, _factory.CreateDateTimeFormater()));
            string expected = "select * from User where Date>'2018-09-09' and Date<'2019-10-10'";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestDateTimeDiff()
        {
            Setup();
            DateTime d = new DateTime(2009, 9, 1, 6, 15, 12);
            _maker.AddAndCondition(new DateDiffLessThanMinutesCondition("shengchandingxingriqi", d, 5, _factory.CreateDateTimeFormater()));
            string expected =
                "select * from User where DATEDIFF(Minute, '2009-09-01 06:15:12', shengchandingxingriqi)<5";
            string actual = _maker.MakeSelectSql();

            Assert.AreEqual(expected, actual);
        }
    }
}