namespace KevPOS.ValueObjects
{
    public sealed class Quantity
    {
        public Quantity(int quantity)
        {
            Value = quantity;
        }

        private int Value { get; set; }

        public int ToInt()
        {
            return Value;
        }
    }
}