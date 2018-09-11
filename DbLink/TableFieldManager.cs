using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbLink
{
    internal class TableFieldManager
    {
        private readonly IDateTimeFormater _dateTimeformater;
        private readonly List<PropertyFieldMap> _maps;
        private readonly ActiveRecord _activeRecord;

        public TableFieldManager(ActiveRecord record, IDateTimeFormater dateTimeformater)
        {
            _dateTimeformater = dateTimeformater;
            _activeRecord = record;
            _maps = new List<PropertyFieldMap>();
        }

        public IEnumerable<TableField> CreateTableFields()
        {
            var propertyInfos = _activeRecord.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                TableField field = MapPropertyToTableField(propertyInfo);
                AddMap(propertyInfo, field);
                yield return field;
            }
        }

        private void AddMap(PropertyInfo info, TableField field)
        {
            PropertyFieldMap map = new PropertyFieldMap(_activeRecord, info, field);
            _maps.Add(map);
        }

        private TableField MapPropertyToTableField(PropertyInfo property)
        {
            if(property.PropertyType == typeof(uint?))
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
                return new DateTimeField(property.Name, null, _dateTimeformater);
            }

            throw new Exception($"不支持的类型{property.PropertyType}");
        }

        public void UpdateFields()
        {
            foreach (var map in _maps)
            {
                map.UpdateFieldValue();
            }
        }
    }

    internal class PropertyFieldMap
    {
        private readonly ActiveRecord _record;
        private readonly PropertyInfo _propertyInfo;
        private readonly TableField _field;

        public PropertyFieldMap(ActiveRecord record ,PropertyInfo propertyInfo, TableField field)
        {
            _record = record;
            _propertyInfo = propertyInfo;
            _field = field;
        }

        public void UpdateFieldValue()
        {
            object value = _propertyInfo.GetValue(_record);
            _field.SetValue(value);
        }
    }
}
