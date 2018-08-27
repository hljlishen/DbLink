using System;

namespace DbLink
{
    public class DateDiffLessThanMinutesCondition : SelectCondition
    {
        private readonly DateTime? _dateTimeReference;
        private readonly int? _timeSpanInMinute;
        private readonly IDateTimeFormater _formater;

        public DateDiffLessThanMinutesCondition(string fieldName, DateTime? dateTimeReference, int? timeSpanInMinute, IDateTimeFormater formater) :base(fieldName)
        {
            _dateTimeReference = dateTimeReference;
            _timeSpanInMinute = timeSpanInMinute;
            _formater = formater ?? throw new Exception("传入的IDateTimeFormater为Null");
        }

        protected override string MakeValidClause()
        {
            string compareDateTimeString = _formater.DateTimeString(_dateTimeReference);
            return $"DATEDIFF(Minute, {compareDateTimeString}, {FieldName})<{_timeSpanInMinute}";
        }

        public override bool IsValidCondition() => _timeSpanInMinute != null && _dateTimeReference != null && _timeSpanInMinute >= 0;
    }
}
