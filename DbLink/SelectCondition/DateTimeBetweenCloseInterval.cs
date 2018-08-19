using System;

namespace DbLink
{
    public class DateTimeBetweenCloseInterval : SelectCondition
    {
        private readonly DateTime? _begin;
        private readonly DateTime? _end;
        public IDateTimeFormater DateTimeFormater { get; set; }    //初始化之后应立即设置formater

        public DateTimeBetweenCloseInterval(string fieldName, DateTime? begin, DateTime? end, IDateTimeFormater dateTimeFormater) :base(fieldName)
        {
            _begin = begin;
            _end = end;
            DateTimeFormater = dateTimeFormater;
        }

        public override string MakeClause()
        {
            if (DateTimeFormater == null)
                throw new Exception("没有设置有效的DateTimeFormater");
            if (!IsValidCondition()) return "";
            string begin = DateTimeFormater.DateTimeString(_begin);
            string end = DateTimeFormater.DateTimeString(_end);
            return $"{FieldName}>={begin} and {FieldName}<={end}";
        }

        public override bool IsValidCondition() => _begin != null && _end != null;
    }
}