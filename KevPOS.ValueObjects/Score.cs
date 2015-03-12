namespace KevPOS.ValueObjects
{
    public sealed class Score
    {
        public Score(decimal score)
        {
            Value = score;
        }

        private decimal Value { get; set; }

        public decimal ToDecimal()
        {
            return Value;
        }
    }
}