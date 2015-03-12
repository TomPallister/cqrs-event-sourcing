namespace KevPOS.ValueObjects
{
    public sealed class Attribute
    {
        public Attribute(string type, string value)
        {
            Type = type;
            Value = value;
        }

        private string Type { get; set; }
        private string Value { get; set; }

        public string ToType()
        {
            return Type;
        }

        public string ToValue()
        {
            return Value;
        }
    }
}