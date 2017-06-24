using System;

namespace MB.Code.Mvc
{
    public class ObjectConverterFactory
    {
        public static ObjectConverter GetObjectConverter(Type type, ObjectValidater validater)
        {
            switch (TypeResolver.GetObjectType(type))
            {
                case ObjectType.Value:
                    return new ValueTypeConverter(type, validater);
                case ObjectType.SingleClass:
                    return new SingleClassConverter(type, validater);
                case ObjectType.Array:
                    return new ArrayConverter(type, validater);
                case ObjectType.GenericList:
                    return new GenericListConverter(type, validater);
                case ObjectType.GenericDictionary:
                    return new GenericDictionaryConverter(type, validater);
                default:
                    throw new Exception(string.Format("类型转换器未指定类型{0}", type.Name));
            }
        }
    }
}
