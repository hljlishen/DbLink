namespace DbLink
{
    public class DoubleBetween : SelectCondition
    {
        private readonly double? _max;
        private readonly double? _min;

        public DoubleBetween(string fieldName, double max, double min) : base(fieldName)
        {
            _max = max;
            _min = min;
        }

        public DoubleBetween(string fieldName, string maxStringNullable, string minStringNullable) : base(fieldName)
        {
            if (!string.IsNullOrEmpty(maxStringNullable))
                _max = int.Parse(maxStringNullable);
            if (!string.IsNullOrEmpty(minStringNullable))
                _min = int.Parse(minStringNullable);
        }

        public override string MakeClause() =>IsValidCondition() ? $"{FieldName}>{_min} and {FieldName}<{_max}" : "";
        public override bool IsValidCondition() => _max != null && _min != null;
    }
}