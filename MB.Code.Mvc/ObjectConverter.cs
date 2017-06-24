using System;

namespace MB.Code.Mvc
{
    public abstract class ObjectConverter
    {
        protected Type Type
        {
            get;
            set;
        }
        protected ObjectValidater Validater
        {
            get;
            set;
        }
        public abstract object ConvertTo(object value);
        protected virtual void Validate(object value)
        {
            if (this.Validater != null)
            {
                this.Validater.Validate(value);
            }
        }
        protected static bool IsNullOrEmpty(object value)
        {
            return value == null || object.Equals(value, string.Empty);
        }
    }
}