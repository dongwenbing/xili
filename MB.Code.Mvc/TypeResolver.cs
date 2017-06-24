using System;

namespace MB.Code.Mvc
{
    public class TypeResolver
    {
        public static ObjectType GetObjectType(Type type)
        {
            if (TypeResolver.IsExtendValueType(type))
            {
                return ObjectType.Value;
            }
            if (TypeResolver.IsGenericDictionaryType(type))
            {
                return ObjectType.GenericDictionary;
            }
            if (TypeResolver.IsGenericListType(type))
            {
                return ObjectType.GenericList;
            }
            if (TypeResolver.IsArray(type))
            {
                return ObjectType.Array;
            }
            if (TypeResolver.IsSingleClass(type))
            {
                return ObjectType.SingleClass;
            }
            return ObjectType.UnDefine;
        }
        public static bool IsGenericListType(Type type)
        {
            bool isRight = false;
            if (type.IsGenericType)
            {
                isRight = TypeResolver.IsList(type);
            }
            return isRight;
        }
        public static bool IsGenericDictionaryType(Type type)
        {
            bool isRight = false;
            if (type.IsGenericType)
            {
                isRight = TypeResolver.IsDictionary(type);
            }
            return isRight;
        }
        public static bool IsArray(Type type)
        {
            return type.IsArray;
        }
        public static bool IsSingleClass(Type type)
        {
            return !TypeResolver.IsSpecialValueType(type) && !TypeResolver.IsList(type) && !TypeResolver.IsDictionary(type) && !TypeResolver.IsArray(type) && type.IsClass;
        }
        public static bool IsDictionary(Type type)
        {
            return type.Name.StartsWith("Dictionary", StringComparison.CurrentCultureIgnoreCase);
        }
        public static bool IsList(Type type)
        {
            return type.Name.StartsWith("List", StringComparison.CurrentCultureIgnoreCase);
        }
        public static bool IsExtendValueType(Type type)
        {
            return type.IsValueType || TypeResolver.IsSpecialValueType(type);
        }
        public static bool IsSpecialValueType(Type type)
        {
            return TypeResolver.IsStr(type) || TypeResolver.IsObj(type);
        }
        public static bool IsStr(Type type)
        {
            Type typeStr = typeof(string);
            return type == typeStr;
        }
        public static bool IsEnum(Type type)
        {
            return type.IsEnum;
        }
        public static bool IsTimeSpan(Type type)
        {
            return type == typeof(TimeSpan) || type == typeof(TimeSpan?);
        }
        public static bool IsNullable(Type type)
        {
            return type.Name.StartsWith("Nullable", StringComparison.CurrentCultureIgnoreCase);
        }
        public static bool IsObj(Type type)
        {
            Type typeObj = typeof(object);
            return type == typeObj;
        }
    }
}
