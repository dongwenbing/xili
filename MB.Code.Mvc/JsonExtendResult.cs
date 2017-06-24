using MB.Code.Common.Json;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace MB.Code.Mvc
{
    public class JsonExtendResult : JsonResult
    {
        private string _dateFmt = "yyyy-MM-dd HH:mm:ss";
        private int _decimalPlaces = 2;
        public JsonExtendResult(object data, JsonRequestBehavior behavior)
        {
            base.Data = data ;
            base.JsonRequestBehavior=behavior;
        }
        public JsonExtendResult(object data, JsonRequestBehavior behavior, string dateFmt) : this(data, behavior)
        {
            this._dateFmt = dateFmt;
        }
        public JsonExtendResult(object data, JsonRequestBehavior behavior, int decimalPlaces) : this(data, behavior)
        {
            this._decimalPlaces = decimalPlaces;
        }
        public JsonExtendResult(object data, JsonRequestBehavior behavior, string dateFmt, int decimalPlaces) : this(data, behavior)
        {
            this._dateFmt = dateFmt;
            this._decimalPlaces = decimalPlaces;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
          //  if (int.Parse(base.JsonRequestBehavior.ToString())  == 1 && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                if (base.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                {
                throw new InvalidOperationException("当前请求不允许Get方法");
            }
            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(base.ContentType))
            {
                response.ContentType = base.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (base.ContentEncoding != null)
            {
                response.ContentEncoding = base.ContentEncoding;
            }
            if (base.Data != null)
            {
                DecimalJsonConverter decimalConverter = new DecimalJsonConverter(this._decimalPlaces);
                DoubleJsonConverter doubleConverter = new DoubleJsonConverter(this._decimalPlaces);
                DateTimeJsonConverter dateTimeConverter = new DateTimeJsonConverter(this._dateFmt);
                MySqlDateTimeJsonConverter mySqlDateTimeConverter = new MySqlDateTimeJsonConverter(this._dateFmt);
                string json = JsonConvert.SerializeObject(base.Data, new JsonConverter[]
                {
                    dateTimeConverter,
                    mySqlDateTimeConverter,
                    decimalConverter,
                    doubleConverter
                });
                response.Write(json);
            }
        }
    }
}
