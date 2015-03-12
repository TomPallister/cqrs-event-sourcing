using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KevPOS.ValueObjects.Converters
{
    public class NameConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Name);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            var name = obj.GetValue("Name").Value<string>();
            return new Name(name);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var name = (Name) value;
            writer.WriteStartObject();
            writer.WritePropertyName("Name");
            writer.WriteValue(name.ToString());
            writer.WriteEndObject();
        }
    }
}