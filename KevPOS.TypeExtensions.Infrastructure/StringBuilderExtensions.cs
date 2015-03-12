using System.Text;

namespace KevPOS.TypeExtensions.Infrastructure
{
    public static class StringBuilderExtension
    {
        public static void AppendCsvField(this StringBuilder sb, int fieldValue)
        {
            sb.AppendFormat("{0},", fieldValue);
        }

        public static void AppendCsvField(this StringBuilder sb, decimal fieldValue)
        {
            sb.AppendFormat("{0},", fieldValue);
        }

        public static void AppendCsvField(this StringBuilder sb, double fieldValue)
        {
            sb.AppendFormat("{0},", fieldValue);
        }

        public static void AppendCsvField(this StringBuilder sb, string fieldValue)
        {
            sb.AppendFormat("{0},", fieldValue.Replace("\"", "'").Trim());
        }

        public static void AppendCsvTextField(this StringBuilder sb, string fieldValue)
        {
            sb.AppendFormat("\"{0}\",", fieldValue.Replace("\"", "'").Trim());
        }

        public static void AppendCsvLineEnd(this StringBuilder sb, int fieldValue)
        {
            sb.AppendFormat("{0}\r\n", fieldValue);
        }

        public static void AppendCsvLineEnd(this StringBuilder sb, string fieldValue)
        {
            sb.AppendFormat("{0}\r\n", fieldValue.Replace("\"", "'").Trim());
        }

        public static void AppendCsvTextLineEnd(this StringBuilder sb, string fieldValue)
        {
            sb.AppendFormat("\"{0}\"\r\n", fieldValue.Replace("\"", "'").Trim());
        }
    }
}