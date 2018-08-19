using System;

namespace DbLink
{
    public class DateEqual : SelectCondition
    {
        private readonly DateTime? _dateTime;
        public IDateTimeFormater DateTimeFormater { get; set; }    //初始化之后应立即设置formater

        public DateEqual(string fieldName, DateTime? dateTime, IDateTimeFormater dateTimeFormater) : base(fieldName)
        {
            _dateTime = dateTime;
            DateTimeFormater = dateTimeFormater;
        }
        public override string MakeClause()
        {
            if(DateTimeFormater == null)
                throw new Exception("没有设置有效的DateTimeFormater");

            return IsValidCondition() ? $"{FieldName}={DateTimeFormater.DateString(_dateTime)}" : "";
        }

        public override bool IsValidCondition() => _dateTime != null;
    }
}