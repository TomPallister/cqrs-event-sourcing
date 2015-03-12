namespace KevPOS.ValueObjects
{
    public class Text
    {
        public Text(string text)
        {
            Value = text;
        }

        private string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}