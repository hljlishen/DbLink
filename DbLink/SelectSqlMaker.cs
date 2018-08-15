using System;
using System.Collections.Generic;

namespace DbLink
{
    public class SelectSqlMaker
    {
        private readonly string _tableName;
        private readonly List<SelectCondition> _andConditions;
        private readonly List<SelectCondition> _orConditions;
        private readonly List<string> _selectFields;
        private int _fieldsIndex;
        private const string Space = " ";

        public SelectSqlMaker(string tableName)
        {
            _tableName = tableName;
            _andConditions = new List<SelectCondition>();
            _orConditions = new List<SelectCondition>();
            _selectFields = new List<string>();
        }

        public string MakeSelectSql()
        {
            return NeedWhereClause() ? MakeSelectSqlWithWhereClause(): MakeSelectSqlWithoutWhereClause();
        }

        protected bool NeedWhereClause() => _andConditions.Count != 0 || _orConditions.Count != 0;

        private string MakeSelectSqlWithWhereClause() => (MakeSelectClause() + MakeWhereClause()).TrimEnd();

        private string MakeSelectSqlWithoutWhereClause() => MakeSelectClause().TrimEnd();

        private string MakeSelectClause()
        {
            string selectClause = "select" + Space;
            selectClause += MakeSelectFields();
            selectClause += $"from {_tableName} ";
            return selectClause;
        }

        private string MakeSelectFields()
        {
            string fieldsString = "";
            if (_selectFields.Count == 0)
                fieldsString = "*";
            else
            {
                for (_fieldsIndex = 0; _fieldsIndex < _selectFields.Count; _fieldsIndex++)
                {
                    fieldsString += _selectFields[_fieldsIndex];
                    if (IsThereOtherFields())
                        fieldsString += ",";
                }
            }

            fieldsString += Space;
            return fieldsString;
        }

        private bool IsThereOtherFields() => _fieldsIndex != _selectFields.Count - 1;

        protected string MakeWhereClause()
        {
            string whereClause = "where" + Space;

            if (NeedAndConditionClause())
            {
                whereClause += MakeAndConditionClause();
                if (NeedOrConditionClause())
                {
                    whereClause += "or" + Space;
                }
            }

            if (NeedOrConditionClause())
            {
                whereClause += MakeOrConditionClause();
            }

            return whereClause;
        }

        private bool NeedAndConditionClause() => _andConditions.Count != 0;

        private string MakeAndConditionClause()
        {
            string andConditionClause = "";
            foreach (var andCondition in _andConditions)
            {
                andConditionClause += andCondition.MakeClause() + Space;
                if (IsThereOtherAndConditions(andCondition))
                    andConditionClause += "and" + Space;
            }

            return andConditionClause;
        }

        private bool IsThereOtherAndConditions(SelectCondition andCondition) => andCondition != _andConditions[_andConditions.Count - 1];


        private bool NeedOrConditionClause() => _orConditions.Count != 0;

        private string MakeOrConditionClause()
        {
            string orConditionClause = "";
            foreach (var orCondition in _orConditions)
            {
                orConditionClause += orCondition.MakeClause() + Space;
                if (IsThereOtherOrConditions(orCondition))
                    orConditionClause += "or" + Space;
            }
            return orConditionClause;
        }

        private bool IsThereOtherOrConditions(SelectCondition orCondition) =>
            orCondition != _orConditions[_orConditions.Count - 1];

        public void AddAndCondition(SelectCondition condition) => _andConditions.Add(condition);

        public void AddOrCondition(SelectCondition condition) => _orConditions.Add(condition);

        public void AddFieldsWillBeSelected(string field)
        {
            if(!_selectFields.Contains(field))
                _selectFields.Add(field);
            else
            {
                throw new Exception($"{field}不能重复添加");
            }
        }

    }
}
