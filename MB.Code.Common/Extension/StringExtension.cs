using System;

namespace MB.Code.Common.Extension
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        public static int? ToInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            int num = 0;
            int.TryParse(str, out num);
            return new int?(num);
        }
        public static long? ToLong(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            long num = 0L;
            long.TryParse(str, out num);
            return new long?(num);
        }
        public static bool? ToBool(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            bool b = false;
            bool.TryParse(str, out b);
            return new bool?(b);
        }
    }
}
