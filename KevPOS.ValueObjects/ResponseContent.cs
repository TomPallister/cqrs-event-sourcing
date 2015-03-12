using System.Collections.Generic;

namespace KevPOS.ValueObjects
{
    public class ResponseContent
    {
        public long AggregateId;
        public List<string> ErrorMessages;
        public bool Valid;
    }
}