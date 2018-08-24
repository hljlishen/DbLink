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
        protected override string MakeValidClause() => $"{FieldName}={DateTimeFormater.DateString(_dateTime)}";

        public override bool IsValidCondition() => _dateTime != null && DateTimeFormater != null;
    }
}