using System;
using System.Globalization;
using System.Web.Mvc;

namespace MB.Code.Mvc
{
    public class JsonStringProviderResult : ValueProviderResult
    {
        public JsonStringProviderResult(object rawValue, string attemptedValue, CultureInfo culture) : base(rawValue, attemptedValue, culture)
        {
        }
        public override object ConvertTo(Type type, CultureInfo culture)
        {
            ObjectConverter converter = ObjectConverterFactory.GetObjectConverter(type, null);
            return converter.ConvertTo(base.RawValue);
        }
    }
}
