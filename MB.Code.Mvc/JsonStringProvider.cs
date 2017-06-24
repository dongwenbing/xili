using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace MB.Code.Mvc
{
    public class JsonStringProvider<TValue> : IValueProvider
    {
        private IDictionary<string, TValue> Dictionary
        {
            get;
            set;
        }
        private CultureInfo CultureInfo
        {
            get;
            set;
        }
        public JsonStringProvider(IDictionary<string, TValue> dictionary, CultureInfo cultureInfo)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            this.Dictionary = dictionary;
        }
        public bool ContainsPrefix(string prefix)
        {
            return this.Dictionary.ContainsKey(prefix);
        }
        public ValueProviderResult GetValue(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            JsonStringProviderResult result = null;
            if (string.IsNullOrEmpty(key))
            {
                result = new JsonStringProviderResult(this.Dictionary, string.Empty, this.CultureInfo);
            }
            else
            {
                TValue value;
                if (this.Dictionary.TryGetValue(key, out value))
                {
                    result = new JsonStringProviderResult(value, string.Empty, this.CultureInfo);
                }
            }
            return result;
        }
    }
}
