using System.Collections.Generic;
using System.Text;

namespace FirstOneTo.Helpers
{
    public static class AzureListStringHelpers
    {
        public static string CreateDelimitedString(IEnumerable<string> items)
        {
            var sb = new StringBuilder();

            foreach (string item in items)
            {
                sb.Append(item.Replace("\\", "\\\\").Replace(",", "\\,"));
                sb.Append(",");
            }

            return (sb.Length > 0) ? sb.ToString(0, sb.Length - 1) : string.Empty;
        }

        public static IEnumerable<string> GetItemsFromDelimitedString(string s)
        {
            bool escaped = false;
            var sb = new StringBuilder();

            foreach (char c in s)
            {
                if ((c == '\\') && !escaped)
                {
                    escaped = true;
                }
                else if ((c == ',') && !escaped)
                {
                    yield return sb.ToString();
                    sb.Length = 0;
                }
                else
                {
                    sb.Append(c);
                    escaped = false;
                }
            }

            yield return sb.ToString();
        }
    }
}