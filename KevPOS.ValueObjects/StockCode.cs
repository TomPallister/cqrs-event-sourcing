namespace KevPOS.ValueObjects
{
    public sealed class StockCode
    {
        public StockCode(string stockCode)
        {
            Value = stockCode;
        }

        private string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}