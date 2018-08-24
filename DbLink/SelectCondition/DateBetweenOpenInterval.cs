using System;

namespace DbLink
{
    public class DateBetweenOpenInterval : SelectCondition
    {
        private readonly DateTime? _begin;
        private readonly DateTime? _end;
        private readonly IDateTimeFormater _dateTimeFormater;

        public DateBetweenOpenInterval(string fieldName, DateTime? begin, DateTime? end, IDateTimeFormater formater) : base(fieldName)
        {
            _begin = begin;
            _end = end;
            _dateTimeFormater = formater;
        }

        protected override string MakeValidClause() => $"{FieldName}>{_dateTimeFormater.DateString(_begin)} and {FieldName}<{_dateTimeFormater.DateString(_end)}";

        public override bool IsValidCondition() => _begin != null && _end != null && _dateTimeFormater != null;
    }
}
