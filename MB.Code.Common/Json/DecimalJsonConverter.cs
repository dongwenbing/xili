using Newtonsoft.Json;
using System;

namespace MB.Code.Common.Json
{
    public class DecimalJsonConverter : JsonConverter
    {
        public int DecimalPlaces
        {
            get;
            set;
        }
        public DecimalJsonConverter()
        {
            this.DecimalPlaces = 2;
        }
        public DecimalJsonConverter(int decimalPlaces)
        {
            this.DecimalPlaces = decimalPlaces;
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(decimal?);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                value = 0;
            }
            string fmt = "{0:f" + this.DecimalPlaces + "}";
            writer.WriteValue(string.Format(fmt, value));
        }
    }
}
