using System;
using System.Globalization;

namespace KevPOS.TypeExtensions.Infrastructure
{
    public static class Int32Extensions
    {
        public static string PadLeft(this int value, int totalWidth, char paddingChar)
        {
            string valueString = value.ToString(CultureInfo.InvariantCulture);
            return valueString.Length > totalWidth ? valueString : valueString.PadLeft(totalWidth, paddingChar);
        }

        public static string ToParcelNumber(this int value)
        {
            return value.PadLeft(6, '0');
        }

        public static void Times(this int iterations, Action action)
        {
            for (int i = 0; i < iterations; i++)
                action.Invoke();
        }

        public static void Times(this int iterations, Action<int> action)
        {
            for (int i = 0; i < iterations; i++)
                action.Invoke(i);
        }
    }
}