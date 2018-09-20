using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbLink
{
    internal class TableFieldPropertyMap
    {
        private readonly List<PropertyInfo> _properties;
        private readonly List<TableField> _tableFields;
        private readonly ActiveRecord _activeRecord;

        public TableFieldPropertyMap(ActiveRecord record)
        {
            _activeRecord = record;
            _properties = new List<PropertyInfo>();
            _tableFields = new List<TableField>();
        }

        public void AddMap(PropertyInfo info, TableField field)
        {
            _properties.Add(info);
            _tableFields.Add(field);
        }

        public void UpdateFields()
        {
            for(int index = 0; index < _properties.Count; index ++)
            {
                object value = _properties[index].GetValue(_activeRecord);
                _tableFields[index].SetValue(value);
            }
        }
    }
}
