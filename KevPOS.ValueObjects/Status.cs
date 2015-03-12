namespace KevPOS.ValueObjects
{
    public sealed class Status
    {
        public Status(State state)
        {
            Value = state;
        }

        private State Value { get; set; }

        public State ToState()
        {
            return Value;
        }
    }
}