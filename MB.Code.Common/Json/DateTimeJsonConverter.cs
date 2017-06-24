using Newtonsoft.Json;
using System;

namespace MB.Code.Common.Json
{
    public class DateTimeJsonConverter : JsonConverter
    {
        public string DateFmt
        {
            get;
            set;
        }
        public DateTimeJsonConverter()
        {
            this.DateFmt = "yyyy-MM-dd HH:mm:ss";
        }
        public DateTimeJsonConverter(string dateFmt)
        {
            this.DateFmt = dateFmt;
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string json = string.Empty;
            if (value != null)
            {
                string fmt = "{0:" + this.DateFmt + "}";
                json = string.Format(fmt, value);
            }
            writer.WriteValue(json);
        }
    }
}
