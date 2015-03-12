using System.Text;

namespace KevPOS.TypeExtensions.Infrastructure
{
    public static class StringExtensions
    {
        public static string ToFullTextFullTerm(this string text)
        {
            return string.Format("\"{0}*\"", text.Trim());
        }

        public static string ToFullTextAndTerm(this string text)
        {
            return text.ToFullTextTerm("AND");
        }

        public static string ToFullTextOrTerm(this string text)
        {
            return text.ToFullTextTerm("OR");
        }

        public static string ToFullTextTerm(this string text, string searchOperator)
        {
            string[] words = text.Split(' ');

            var sb = new StringBuilder();
            foreach (string word in words)
            {
                if (sb.Length > 0)
                {
                    sb.AppendFormat(" {0} ", searchOperator);
                }
                sb.AppendFormat("\"{0}*\"", word);
            }

            return sb.ToString();
        }
    }
}