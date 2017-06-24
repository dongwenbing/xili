using System;
using System.Collections;
using System.Linq;

namespace MB.Code.Mvc
{
    public class GenericListConverter : ObjectConverter
    {
        public GenericListConverter(Type type, ObjectValidater validater)
        {
            base.Type = type;
            base.Validater = validater;
        }
        public override object ConvertTo(object value)
        {
            this.Validate(value);
            Type type = base.Type.GetGenericArguments().FirstOrDefault<Type>();
            if (type == null)
            {
                throw new Exception(string.Format("类型{0}不是List泛型", base.Type.Name));
            }
            IList list = value as IList;
            if (list == null)
            {
                return null;
            }
            IList listResult = Activator.CreateInstance(base.Type) as IList;
            ObjectConverter converter = ObjectConverterFactory.GetObjectConverter(type, null);
            foreach (object obj in list)
            {
                object result = converter.ConvertTo(obj);
                listResult.Add(result);
            }
            return listResult;
        }
    }
}
