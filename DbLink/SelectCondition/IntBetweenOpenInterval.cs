namespace DbLink
{
    public class IntBetweenOpenInterval : SelectCondition
    {
        private readonly int? _max;
        private readonly int? _min;

        public IntBetweenOpenInterval(string fieldName, int? max, int? min) : base(fieldName)
        {
            _max = max;
            _min = min;
        }

        public IntBetweenOpenInterval(string fieldName, string maxStringNullable, string minStringNullable) : base(fieldName)
        {
            if (!string.IsNullOrEmpty(maxStringNullable))
                _max = int.Parse(maxStringNullable);
            if (!string.IsNullOrEmpty(minStringNullable))
                _min = int.Parse(minStringNullable);
        }

        protected override string MakeValidClause() =>$"{FieldName}>{_min} and {FieldName}<{_max}" ;
        public override bool IsValidCondition() => _max != null && _min != null;
    }
}