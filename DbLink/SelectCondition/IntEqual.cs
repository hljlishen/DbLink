namespace DbLink
{
    public class IntEqual : SelectCondition
    {
        private readonly int? _intValue = null;

        public IntEqual(string fieldName, int intValue) : base(fieldName)
        {
            _intValue = intValue;
        }

        public IntEqual(string fieldName, string intStringNullable) : base(fieldName)
        {
            if (!string.IsNullOrEmpty(intStringNullable))
                _intValue = int.Parse(intStringNullable);
        }

        protected override string MakeValidClause() => $"{FieldName}={_intValue}";

        public override bool IsValidCondition() => _intValue != null;
    }
}