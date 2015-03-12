namespace KevPOS.ValueObjects
{
    public sealed class OrderStatus
    {
        public OrderStatus(OrderState state)
        {
            Value = state;
        }

        private OrderState Value { get; set; }

        public OrderState ToOrderState()
        {
            return Value;
        }
    }
}