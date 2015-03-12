namespace KevPOS.ValueObjects
{
    public class Email
    {
        public Email(string email)
        {
            Value = email;
        }

        private string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}