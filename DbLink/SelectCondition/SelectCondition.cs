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

        public abstract string MakeClause();

        public abstract bool IsValidCondition();
    }
}