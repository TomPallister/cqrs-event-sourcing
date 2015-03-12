using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KevPOS.ValueObjects.Converters
{
    public class IdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Id);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            var id = obj.GetValue("Id").Value<Int64>();
            return new Id(id);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var id = (Id) value;
            writer.WriteStartObject();
            writer.WritePropertyName("Id");
            writer.WriteValue(id.ToLong());
            writer.WriteEndObject();
        }
    }
}