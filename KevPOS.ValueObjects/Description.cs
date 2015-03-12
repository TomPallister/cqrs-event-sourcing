namespace KevPOS.ValueObjects
{
    public sealed class Description
    {
        public Description(string descripton)
        {
            Value = descripton;
        }

        private string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}