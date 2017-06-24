using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MB.Code.Mvc
{
    public class ObjectValidaterFactory
    {
        private static Hashtable _proValidation = Hashtable.Synchronized(new Hashtable(StringComparer.CurrentCultureIgnoreCase));
        private static Hashtable _type = Hashtable.Synchronized(new Hashtable());
        private static string _proFormat = "{0}_{1}";
        private string TypeName
        {
            get;
            set;
        }
        private bool IsNeedValidate
        {
            get;
            set;
        }
        public ObjectValidaterFactory(Type type)
        {
            this.TypeName = type.FullName;
            this.CachedValidationAttribute(type);
            this.IsNeedValidate = (bool)ObjectValidaterFactory._type[this.TypeName];
        }
        private void CachedValidationAttribute(Type type)
        {
            if (!ObjectValidaterFactory._type.ContainsKey(this.TypeName))
            {
                PropertyInfo[] modelProperties = type.GetProperties();
                MetadataTypeAttribute metaAttribute = Attribute.GetCustomAttribute(type, typeof(MetadataTypeAttribute)) as MetadataTypeAttribute;
                if (metaAttribute != null)
                {
                    Type metadataType = metaAttribute.MetadataClassType;
                    modelProperties = metadataType.GetProperties();
                }
                Type typeValidationAttribute = typeof(ValidationAttribute);
                int count = 0;
                PropertyInfo[] array2 = modelProperties;
                for (int i = 0; i < array2.Length; i++)
                {
                    PropertyInfo pro = array2[i];
                    string key = string.Format(ObjectValidaterFactory._proFormat, this.TypeName, pro.Name);
                    Attribute[] array = Attribute.GetCustomAttributes(pro, typeValidationAttribute);
                    count += array.Length;
                    ObjectValidaterFactory._proValidation[key] = array;
                }
                ObjectValidaterFactory._type[this.TypeName] = (count > 0);
            }
        }
        public ObjectValidater GetObjectValidater(PropertyDescriptor pd)
        {
            if (!this.IsNeedValidate)
            {
                return null;
            }
            string key = string.Format(ObjectValidaterFactory._proFormat, this.TypeName, pd.Name);
            ValidationAttribute[] attrs = ObjectValidaterFactory._proValidation[key] as ValidationAttribute[];
            if (attrs != null)
            {
                return new ObjectValidater(attrs, pd);
            }
            return null;
        }
    }
}
