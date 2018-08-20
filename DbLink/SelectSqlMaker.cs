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

        protected bool NeedWhereClause() => NeedAndConditionClause() || NeedOrConditionClause();

        private string MakeSelectSqlWithWhereClause() => (MakeSelectClause() + MakeWhereClause()).TrimEnd().Replace(Space + Space, Space);

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

        private bool NeedAndConditionClause() => HasValidCondition(_andConditions);

        private bool HasValidCondition(List<SelectCondition> conditions)
        {
            if (conditions.Count == 0) return false;

            foreach (SelectCondition condition in conditions)
            {
                if (condition.IsValidCondition())
                    return true;
            }

            return false;
        }

        private string MakeAndConditionClause()
        {
            string andConditionClause = "";
            foreach (var andCondition in _andConditions)
            {
                andConditionClause += andCondition.MakeClause() + Space;
                if (IsThereOtherValidAndConditions(andCondition) && andCondition.IsValidCondition())
                    andConditionClause += "and" + Space;
            }

            return andConditionClause;
        }

        private bool IsThereOtherValidAndConditions(SelectCondition andCondition)
        {
            return IsThereOtherValidConditionAfterCurrentCondition(andCondition, _andConditions);
        }

        private bool NeedOrConditionClause() => HasValidCondition(_orConditions);

        private string MakeOrConditionClause()
        {
            string orConditionClause = "";
            foreach (var orCondition in _orConditions)
            {
                orConditionClause += orCondition.MakeClause() + Space;
                if (IsThereOtherValidOrConditions(orCondition) && orCondition.IsValidCondition())
                    orConditionClause += "or" + Space;
            }
            return orConditionClause;
        }

        private bool IsThereOtherValidOrConditions(SelectCondition orCondition)
        {
            return IsThereOtherValidConditionAfterCurrentCondition(orCondition, _orConditions);
        }

        private bool IsThereOtherValidConditionAfterCurrentCondition(SelectCondition currentCondition, List<SelectCondition> conditions)
        {
            int index = conditions.FindIndex(item => item.Equals(currentCondition));

            if (index == conditions.Count - 1) return false;
            List<SelectCondition> conditionsBehindCurrentCondition = conditions.GetRange(index + 1, conditions.Count - index - 1);
            return HasValidCondition(conditionsBehindCurrentCondition);
        }

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
