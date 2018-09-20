using System;
using System.Globalization;
using System.Reflection;

namespace DbLink
{
    internal abstract class TableField
    {
        protected TableField(string fieldName)
        {
            FieldName = fieldName;
        }

        private string FieldName { get; }
        public object FieldValue { get; set; }

        public bool HasValue() => FieldValue != null;

        public abstract string MakeClause();

        public abstract string GetValueString();

        public abstract void SetValue(object value);

        public string GetFieldName() => FieldName;

        public static TableField Parse(PropertyInfo property, DatabaseType type)
        {
            if (property.PropertyType == typeof(uint?))
            {
                return new UintField(property.Name, null);
            }
            if (property.PropertyType == typeof(int?))
            {
                return new IntField(property.Name, null);
            }

            if (property.PropertyType == typeof(string))
            {
                return new StringField(property.Name, null);
            }

            if (property.PropertyType == typeof(double?) || property.PropertyType == typeof(float?))
            {
                return new DoubleField(property.Name, null);
            }

            if (property.PropertyType == typeof(DateTime?))
            {
                return new DateTimeField(property.Name, null, DbLinkGateway.CreateFactory(type).CreateDateTimeFormater());
            }

            throw new Exception($"不支持的类型{property.PropertyType}");
        }
    }

    internal class StringField : TableField
    {
        public StringField(string fieldName, string fieldFieldValue) : base(fieldName)
        {
            FieldValue = fieldFieldValue;
        }

        public override string MakeClause() => $"{GetFieldName()}='{FieldValue}'";
        public override string GetValueString() => $"'{FieldValue}'";
        public override void SetValue(object value)
        {
            try
            {
                string str = (string) value;
                FieldValue = str;
            }
            catch
            {
                throw  new Exception("value值不是string类型");
            }
        }
    }

    internal class IntField : TableField
    {
        public IntField(string fieldName, int? fieldFieldValue) : base(fieldName)
        {
            FieldValue = fieldFieldValue;
        }

        public override string MakeClause() => $"{GetFieldName()}={FieldValue}";
        public override string GetValueString() => FieldValue.ToString();
        public override void SetValue(object value)
        {
            try
            {
                int? intValue = (int?)value;
                FieldValue = intValue;
            }
            catch
            {
                throw new Exception("value值不是int类型");
            }
        }
    }

    internal class UintField : TableField
    {
        public UintField(string fieldName, uint? fieldFieldValue) : base(fieldName)
        {
            FieldValue = fieldFieldValue;
        }

        public override string MakeClause() => $"{GetFieldName()}={FieldValue}";
        public override string GetValueString() => FieldValue.ToString();
        public override void SetValue(object value)
        {
            try
            {
                uint? intValue = (uint?)value;
                FieldValue = intValue;
            }
            catch
            {
                throw new Exception("value值不是uint类型");
            }
        }
    }

    internal class DateTimeField : TableField
    {
        public IDateTimeFormater DateTimeFormater { get; set; }

        public DateTimeField(string fieldName, DateTime? dateAndTime, IDateTimeFormater dateTimeFormater) : base(fieldName)
        {
            FieldValue = dateAndTime;
            DateTimeFormater = dateTimeFormater;
        }

        public override void SetValue(object value)
        {
            try
            {
                FieldValue = (DateTime?)value;
            }
            catch
            {
                throw new Exception("value值不是DateTime类型");
            }
        }

        public override string MakeClause()
        {
            return $"{GetFieldName()}={DateTimeFormater.DateTimeString((DateTime)FieldValue)}";
        }

        public override string GetValueString()
        {
            return DateTimeFormater.DateTimeString((DateTime)FieldValue);
        }
    }

    internal class DateField : TableField
    {
        public IDateTimeFormater DateTimeFormater { get; set; }
        public DateField(string fieldName, DateTime? dateTime, IDateTimeFormater formater) : base(fieldName)
        {
            FieldValue = dateTime;
            DateTimeFormater = formater;
        }

        public override string MakeClause() => $"{GetFieldName()}={DateTimeFormater.DateString((DateTime?)FieldValue)}";

        public override string GetValueString() => DateTimeFormater.DateString((DateTime?)FieldValue);

        public override void SetValue(object value)
        {
            try
            {
                FieldValue = (DateTime?)value;
            }
            catch
            {
                throw new Exception("value值不是DateTime类型");
            }
        }
    }

    internal class DoubleField : TableField
    {
        public DoubleField(string fieldName, double? fieldValue) : base(fieldName)
        {
            FieldValue = fieldValue;
        }

        public override string MakeClause() => $"{GetFieldName()}={FieldValue}";

        public override string GetValueString() => ((double) FieldValue).ToString(CultureInfo.InvariantCulture);

        public override void SetValue(object value) => FieldValue = value;
    }
}
