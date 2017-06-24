using System;
using System.Collections;



namespace MB.Code.Mvc
{
    public class ArrayConverter : ObjectConverter
    {
        public ArrayConverter(Type type, ObjectValidater validater)
        {
            base.Type = type;
            base.Validater = validater;
        }
        public override object ConvertTo(object value)
        {
            this.Validate(value);
            IList array = value as IList;
            if (array == null)
            {
                return null;
            }
            IList arrayConvert = Array.CreateInstance(base.Type, array.Count);
            Type elementType = base.Type.GetElementType();
            ObjectConverter converter = ObjectConverterFactory.GetObjectConverter(elementType, null);
            foreach (object obj in array)
            {
                object objConvert = converter.ConvertTo(obj);
                arrayConvert.Add(objConvert);
            }
            return arrayConvert;
        }
    }
}
