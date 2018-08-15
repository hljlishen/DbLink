using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbLink
{
    public class TableFieldGenerator
    {
        private readonly IDateTimeFormater _dateTimeformater;

        public TableFieldGenerator(IDateTimeFormater dateTimeformater)
        {
            _dateTimeformater = dateTimeformater;
        }

        public IEnumerable<TableField> MapPropertiesToTableFields(ActiveRecord activeRecord)
        {
            var propertyInfos = activeRecord.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                TableField field = MapPropertyToTableField(propertyInfo);
                //BindingProperty(activeRecord, propertyInfo.Name, field);
                yield return field;
            }
        }

        private TableField MapPropertyToTableField(PropertyInfo property)
        {
            if (property.PropertyType == typeof(int?))
            {
                IntField field = new IntField(property.Name, 0);
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

        //private void BindingProperty(ActiveRecord activeRecord, string propertyName, TableField field)
        //{
        //    field.DataBindings.Add("FieldValue", activeRecord, propertyName, false, DataSourceUpdateMode.OnPropertyChanged);
        //}
    }
}
