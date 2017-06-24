using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
namespace MB.Core.Common.Util
{
    public class ModelConvertHelper<T> where T : new()
    {
        public static List<T> ConvertToModel(DataTable dt)
        {
            List<T> ts = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
                PropertyInfo[] propertys = t.GetType().GetProperties();
                PropertyInfo[] array = propertys;
                for (int i = 0; i < array.Length; i++)
                {
                    PropertyInfo pi = array[i];
                    string tempName = pi.Name;
                    if (dt.Columns.Contains(tempName) && pi.CanWrite)
                    {
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            if (pi.PropertyType.IsEnum)
                            {
                                pi.SetValue(t, Enum.Parse(pi.PropertyType, value.ToString()), null);
                            }
                            else
                            {
                                pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType, CultureInfo.CurrentCulture), null);
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
