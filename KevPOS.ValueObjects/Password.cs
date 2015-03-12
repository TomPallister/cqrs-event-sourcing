namespace KevPOS.ValueObjects
{
    public class Password
    {
        public Password(string password)
        {
            Value = password;
        }

        private string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}