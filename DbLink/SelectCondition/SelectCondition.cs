using System;

namespace DbLink
{
    public abstract class SelectCondition
    {
        protected string FieldName;

        protected SelectCondition(string fieldName)
        {
            if(string.IsNullOrEmpty(fieldName))
                throw new Exception("传入的域名是空或者null");
            FieldName = fieldName;
        }

        public string MakeClause() => IsValidCondition() ? MakeValidClause() : MakeInvalidClause();

        protected abstract string MakeValidClause();

        private string MakeInvalidClause() => "";

        public abstract bool IsValidCondition();
    }
}