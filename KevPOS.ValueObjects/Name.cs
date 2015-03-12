namespace KevPOS.ValueObjects
{
    public sealed class Name
    {
        public Name(string name)
        {
            Value = name;
        }

        private string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}