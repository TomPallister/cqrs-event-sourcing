namespace KevPOS.ValueObjects
{
    public sealed class Id
    {
        public static Id NullId = new Id(long.MinValue);

        public Id(long id)
        {
            Value = id;
        }

        private long Value { get; set; }

        public long ToLong()
        {
            return Value;
        }

        public static bool operator ==(Id lhs, Id rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Id lhs, Id rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return string.Format("Id :{0}", Value);
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;

            var otherId = other as Id;
            if (otherId == null)
                return false;
            return Value.Equals(otherId.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}