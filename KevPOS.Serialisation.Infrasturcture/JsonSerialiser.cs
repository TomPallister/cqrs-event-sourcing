using System;
using KevPOS.Messages;
using Newtonsoft.Json;

namespace KevPOS.Serialisation.Infrasturcture
{
    public static class JsonSerialiser
    {
        public static string Serialise<T>(T message)
        {
            return JsonConvert.SerializeObject(message);
        }

        public static AbstractEvent Deserialise(string obj, Type type)
        {
            object d = JsonConvert.DeserializeObject(obj, type);
            return (AbstractEvent) d;
        }
    }
}