namespace KevPOS.ValueObjects
{
    public sealed class Price
    {
        public Price(decimal total)
        {
            Value = total;
        }

        private decimal Value { get; set; }

        public decimal ToDouble()
        {
            return Value;
        }
    }
}