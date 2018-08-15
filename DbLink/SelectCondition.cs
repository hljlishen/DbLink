using System;

namespace DbLink
{
    public abstract class SelectCondition
    {
        protected string FieldName;

        protected SelectCondition(string fieldName)
        {
            if(string.IsNullOrEmpty(fieldName))
                throw new Exception("传入的域名是空或者null");
            FieldName = fieldName;
        }

        public abstract string MakeClause();
    }


    public class IntEqual : SelectCondition
    {
        private readonly int _intValue;

        public IntEqual(string fieldName, int intValue) : base(fieldName)
        {
            _intValue = intValue;
        }

        public override string MakeClause() => $"{FieldName}={_intValue}";
    }

    public class StringEqual : SelectCondition
    {
        private readonly string _strValue;

        public StringEqual(string fieldName, string strValue) : base(fieldName)
        {
            _strValue = strValue;
        }

        public override string MakeClause() => $"{FieldName}='{_strValue}'";
    }

    public class StringLike : SelectCondition
    {
        private readonly string _strValue;

        public StringLike(string fieldName, string strValue) : base(fieldName)
        {
            _strValue = strValue;
        }

        public override string MakeClause() => $"{FieldName} like {_strValue} ";
    }

    public class IntBetweenOpenInterval : SelectCondition
    {
        private readonly int _max;
        private readonly int _min;

        public IntBetweenOpenInterval(string fieldName, int max, int min) : base(fieldName)
        {
            _max = max;
            _min = min;
        }

        public override string MakeClause() => $"{FieldName}>{_min} and {FieldName}<{_max}";
    }

    public class IntBetweenCloseInterval : SelectCondition
    {
        private readonly int _max;
        private readonly int _min;

        public IntBetweenCloseInterval(string fieldName, int max, int min) : base(fieldName)
        {
            _max = max;
            _min = min;
        }

        public override string MakeClause() => $"{FieldName}>={_min} and {FieldName}<={_max}";
    }

    public class DoubleBetween : SelectCondition
    {
        private readonly double _max;
        private readonly double _min;

        public DoubleBetween(string fieldName, double max, double min) : base(fieldName)
        {
            _max = max;
            _min = min;
        }

        public override string MakeClause() => $"{FieldName}>{_min} and {FieldName}<{_max}";
    }

    public class DateEqual : SelectCondition
    {
        protected readonly DateTime DateTime;
        public IDateTimeFormater DateTimeFormater { get; set; }    //初始化之后应立即设置formater

        public DateEqual(string fieldName, DateTime dateTime, IDateTimeFormater dateTimeFormater) : base(fieldName)
        {
            DateTime = dateTime;
            DateTimeFormater = dateTimeFormater;
        }
        public override string MakeClause()
        {
            if(DateTimeFormater == null)
                throw new Exception("构造DateEqual对象之后没有设置DateTimeFormater");

            return $"{FieldName}={DateTimeFormater.DateString(DateTime)}";
        }
    }

    public class DateTimeBetweenCloseInterval : SelectCondition
    {
        private readonly DateTime _begin;
        private readonly DateTime _end;
        public IDateTimeFormater DateTimeFormater { get; set; }    //初始化之后应立即设置formater

        public DateTimeBetweenCloseInterval(string fieldName, DateTime begin, DateTime end, IDateTimeFormater dateTimeFormater) :base(fieldName)
        {
            _begin = begin;
            _end = end;
            DateTimeFormater = dateTimeFormater;
        }

        public override string MakeClause()
        {
            if (DateTimeFormater == null)
                throw new Exception("构造DateTimeBetweenCloseInterval对象之后没有设置DateTimeFormater");
            string begin = DateTimeFormater.DateTimeString(_begin);
            string end = DateTimeFormater.DateTimeString(_end);
            return $"{FieldName}>={begin} and {FieldName}<={end}";
        }
    }
}