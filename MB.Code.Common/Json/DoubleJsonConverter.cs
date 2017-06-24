using Newtonsoft.Json;
using System;

namespace MB.Code.Common.Json
{
    public class DoubleJsonConverter : JsonConverter
    {
        public int DecimalPlaces
        {
            get;
            set;
        }
        public DoubleJsonConverter()
        {
            this.DecimalPlaces = 2;
        }
        public DoubleJsonConverter(int decimalPlaces)
        {
            this.DecimalPlaces = decimalPlaces;
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double) || objectType == typeof(double?);
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
