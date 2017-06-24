using MB.Code.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MB.Code.Mvc
{
    public class SingleClassConverter : ObjectConverter
    {
        public SingleClassConverter(Type type, ObjectValidater validater)
        {
            base.Type = type;
            base.Validater = validater;
        }
        public override object ConvertTo(object value)
        {
            this.Validate(value);
            Dictionary<string, object> dic = value as Dictionary<string, object>;
            if (dic == null)
            {
                return null;
            }
            dic = SingleClassConverter.IgnoreCase(dic);
            object obj = Activator.CreateInstance(base.Type);
            ObjectValidaterFactory validaterFactory = new ObjectValidaterFactory(base.Type);
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(base.Type);
            foreach (PropertyDescriptor pd in pdc)
            {
                object subValue = null;
                dic.TryGetValue(pd.Name, out subValue);
                ObjectValidater validater = validaterFactory.GetObjectValidater(pd);
                if (!ObjectConverter.IsNullOrEmpty(subValue) || validater != null)
                {
                    ObjectConverter converter = ObjectConverterFactory.GetObjectConverter(pd.PropertyType, validater);
                    object result = converter.ConvertTo(subValue);
                    pd.SetValue(obj, result);
                }
            }
            if (obj is IBindModelCallback)
            {
                (obj as IBindModelCallback).OnBindModel();
            }
            return obj;
        }
        public static Dictionary<string, object> IgnoreCase(Dictionary<string, object> dic)
        {
            if (dic == null)
            {
                return null;
            }
            Dictionary<string, object> dicResult = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
            foreach (KeyValuePair<string, object> kv in dic)
            {
                dicResult.Add(kv.Key, kv.Value);
            }
            return dicResult;
        }
    }
}
