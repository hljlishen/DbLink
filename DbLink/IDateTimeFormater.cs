using System;

namespace DbLink
{
    public interface IDateTimeFormater
    {
        string DateString(DateTime? date);
        string DateTimeString(DateTime? dateTime);
    }

    internal class StanderdStyleDateTimeFormater : IDateTimeFormater
    {
        public string DateString(DateTime? date) => $"'{date:yyyy-MM-dd}'";

        public string DateTimeString(DateTime? dateTime) => $"'{dateTime:yyyy-MM-dd HH:mm:ss}'";
    }

    internal class AccessStyleDateTimeFormater : IDateTimeFormater
    {
        public string DateString(DateTime? date) => $"#{date:yyyy-MM-dd}#";

        public string DateTimeString(DateTime? dateTime) => $"#{dateTime:yyyy-MM-dd HH:mm:ss}#";
    }
}