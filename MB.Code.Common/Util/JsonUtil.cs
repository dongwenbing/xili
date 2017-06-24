using Newtonsoft.Json;
using System;
using System.Data;
using System.Text;
namespace MB.Core.Common.Util
{
    public class JsonUtil
    {
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static string Serialize(string key, object value)
        {
            return string.Concat(new string[]
            {
                "{\"",
                key,
                "\":",
                JsonUtil.Serialize(value),
                "}"
            });
        }
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static string SerializerDataTable(DataTable value)
        {
            int rowCount = value.Rows.Count;
            int colCount = value.Columns.Count;
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < rowCount; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                sb.Append("{");
                for (int j = 0; j < colCount; j++)
                {
                    if (j > 0)
                    {
                        sb.Append(",");
                    }
                    string v = JsonUtil.TransferSpeicalChar(ConvertUtil.ToString(value.Rows[i][j], ""));
                    sb.Append(string.Concat(new string[]
                    {
                        "\"",
                        value.Columns[j].ColumnName,
                        "\":\"",
                        v,
                        "\""
                    }));
                }
                sb.Append("}");
            }
            sb.Append("]");
            return sb.ToString();
        }
        public static string SerializerDataTable(string key, DataTable value)
        {
            return string.Concat(new string[]
            {
                "{\"",
                key,
                "\":",
                JsonUtil.SerializerDataTable(value),
                "}"
            });
        }
        public static string TransferSpeicalChar(string value)
        {
            return value.Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"");
        }
    }
}
