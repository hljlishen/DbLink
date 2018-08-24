namespace DbLink
{
    public class StringEqual : SelectCondition
    {
        private readonly string _strValue;

        public StringEqual(string fieldName, string strValue) : base(fieldName)
        {
            _strValue = strValue;
        }

        protected override string MakeValidClause() => $"{FieldName}='{_strValue}'";

        public override bool IsValidCondition() => !string.IsNullOrEmpty(_strValue);
    }
}