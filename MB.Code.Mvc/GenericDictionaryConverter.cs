using System;
using System.Collections;
using System.Collections.Generic;

namespace MB.Code.Mvc
{
    public class GenericDictionaryConverter : ObjectConverter
    {
        public GenericDictionaryConverter(Type type, ObjectValidater validater)
        {
            base.Type = type;
            base.Validater = validater;
        }
        public override object ConvertTo(object value)
        {
            this.Validate(value);
            Type[] arrayType = base.Type.GetGenericArguments();
            if (arrayType.Length != 2)
            {
                throw new Exception(string.Format("类型{0}不是Dictionary泛型", base.Type.Name));
            }
            Type typeKey = arrayType[0];
            Type typeValue = arrayType[1];
            ObjectType objTypeEnum = TypeResolver.GetObjectType(typeKey);
            if (objTypeEnum != ObjectType.Value)
            {
                throw new Exception(string.Format("字典类型{0}的键类型不是值类型", base.Type.Name));
            }
            Dictionary<string, object> dic = value as Dictionary<string, object>;
            if (dic == null)
            {
                return null;
            }
            IDictionary dicResult = Activator.CreateInstance(base.Type) as IDictionary;
            ObjectConverter keyConverter = ObjectConverterFactory.GetObjectConverter(typeKey, null);
            ObjectConverter valueConverter = ObjectConverterFactory.GetObjectConverter(typeValue, null);
            foreach (KeyValuePair<string, object> kv in dic)
            {
                object keyResult = keyConverter.ConvertTo(kv.Key);
                object valueResult = valueConverter.ConvertTo(kv.Value);
                dicResult.Add(keyResult, valueResult);
            }
            return dicResult;
        }
    }
}
