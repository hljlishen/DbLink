namespace DbLink
{
    public class StringLike : SelectCondition
    {
        private readonly string _strValue;

        public StringLike(string fieldName, string strValue) : base(fieldName)
        {
            _strValue = strValue;
        }

        protected override string MakeValidClause() => $"{FieldName} like '%{_strValue}%' ";

        public override bool IsValidCondition() => !string.IsNullOrEmpty(_strValue);
    }
}