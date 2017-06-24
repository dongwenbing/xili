using MySql.Data.Types;
using Newtonsoft.Json;
using System;

namespace MB.Code.Common.Json
{
    public class MySqlDateTimeJsonConverter : JsonConverter
    {
        public string DateFmt
        {
            get;
            set;
        }
        public MySqlDateTimeJsonConverter()
        {
            this.DateFmt = "yyyy-MM-dd HH:mm:ss";
        }
        public MySqlDateTimeJsonConverter(string dateFmt)
        {
            this.DateFmt = dateFmt;
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MySqlDateTime) || objectType == typeof(MySqlDateTime?);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            new MySqlDateTime(DateTime.Now);
            string json = string.Empty;
            if (value != null)
            {
                MySqlDateTime dt = (MySqlDateTime)value;
                if (dt.IsValidDateTime)
                {
                    string fmt = "{0:" + this.DateFmt + "}";
                    json = string.Format(fmt, dt.Value);
                }
            }
            writer.WriteValue(json);
        }
    }
}
