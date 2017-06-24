using System;
using System.ComponentModel;
using System.Globalization;

namespace MB.Code.Mvc
{
    public class ValueTypeConverter : ObjectConverter
    {
        public ValueTypeConverter(Type type, ObjectValidater validater)
        {
            base.Type = type;
            base.Validater = validater;
        }
        public override object ConvertTo(object value)
        {
            this.Validate(value);
            if (!TypeResolver.IsSpecialValueType(base.Type))
            {
                if (ObjectConverter.IsNullOrEmpty(value))
                {
                    if (TypeResolver.IsNullable(base.Type))
                    {
                        return null;
                    }
                }
                else
                {
                    if (TypeResolver.IsEnum(base.Type))
                    {
                        return Enum.Parse(base.Type, value.ToString());
                    }
                    if (TypeResolver.IsTimeSpan(base.Type))
                    {
                        return TimeSpan.Parse(value.ToString());
                    }
                    this.UpdateType();
                    TypeConverter converter = TypeDescriptor.GetConverter(base.Type);
                    if (converter.CanConvertTo(base.Type))
                    {
                        return converter.ConvertTo(value, base.Type);
                    }
                    IConvertible con = value as IConvertible;
                    if (con != null)
                    {
                        return con.ToType(base.Type, CultureInfo.CurrentCulture);
                    }
                }
                throw new Exception(string.Format("{0}类型无法转换成{1}类型", value.GetType().Name, base.Type.Name));
            }
            if (!ObjectConverter.IsNullOrEmpty(value))
            {
                return value.ToString().Trim();
            }
            return value;
        }
        private void UpdateType()
        {
            Type underlyingType = Nullable.GetUnderlyingType(base.Type);
            if (underlyingType != null)
            {
                base.Type = underlyingType;
            }
        }
    }
}
