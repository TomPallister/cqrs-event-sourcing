namespace KevPOS.ValueObjects
{
    public class Html
    {
        public Html(string html)
        {
            Value = html;
        }

        private string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}