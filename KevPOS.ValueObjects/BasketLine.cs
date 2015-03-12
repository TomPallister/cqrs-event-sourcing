namespace KevPOS.ValueObjects
{
    public sealed class BasketLine
    {
        public BasketLine(Id productId, Quantity quantity)
        {
            ProuctId = productId;
            Quantity = quantity;
        }

        private Id ProuctId { get; set; }
        private Quantity Quantity { get; set; }

        public Id ToId()
        {
            return ProuctId;
        }

        public Quantity ToQuantity()
        {
            return Quantity;
        }
    }
}