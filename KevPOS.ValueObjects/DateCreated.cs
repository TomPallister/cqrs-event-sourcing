using System;
using System.Globalization;

namespace KevPOS.ValueObjects
{
    public sealed class DateCreated
    {
        public DateCreated(DateTime dateTime)
        {
            Value = dateTime;
        }

        private DateTime Value { get; set; }

        public DateTime ToDateTime()
        {
            return Value;
        }

        public override string ToString()
        {
            return Convert.ToString(Value, CultureInfo.InvariantCulture);
        }
    }
}