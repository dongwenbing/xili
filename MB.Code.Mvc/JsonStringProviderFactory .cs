using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MB.Code.Mvc
{
    public class JsonStringProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            NameValueCollection nvcForm = controllerContext.RequestContext.HttpContext.Request.Form;
            NameValueCollection nvcQueryString = controllerContext.RequestContext.HttpContext.Request.QueryString;
            Dictionary<string, object> dic = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
            this.AddToDic(nvcForm, dic);
            this.AddToDic(nvcQueryString, dic);
            if (dic.Count > 0)
            {
                return new JsonStringProvider<object>(dic, CultureInfo.CurrentCulture);
            }
            return null;
        }
        private bool IsJson(string value)
        {
            value = value.Trim();
            return (value.StartsWith("{") && value.EndsWith("}")) || (value.StartsWith("[") && value.EndsWith("]"));
        }
        private void AddToDic(NameValueCollection nvc, Dictionary<string, object> dic)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (string nv in nvc)
            {
                string strValue = nvc[nv];
                object value;
                if (!this.IsJson(strValue))
                {
                    value = strValue;
                }
                else
                {
                    value = serializer.Deserialize<object>(strValue);
                }
                if (value != null)
                {
                    dic.Add(nv, value);
                }
            }
        }
    }
}
