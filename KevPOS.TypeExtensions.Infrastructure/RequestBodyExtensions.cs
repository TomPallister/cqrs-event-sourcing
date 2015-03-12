using System.IO;
using Nancy;

namespace KevPOS.TypeExtensions.Infrastructure
{
    public static class RequestBodyExtensions
    {
        public static string AsString(this Request request)
        {
            string bodyAsString;
            request.Body.Position = 0;
            using (var rdr = new StreamReader(request.Body))
            {
                bodyAsString = rdr.ReadToEnd();
            }
            return bodyAsString;
        }
    }
}