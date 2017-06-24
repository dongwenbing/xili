
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MB.Code.Mvc
    {
        public class ModelValidater
        {
            private static Hashtable _proValidation = Hashtable.Synchronized(new Hashtable());
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
            public ModelValidater(Type type)
            {
                this.TypeName = type.FullName;
                this.CachedValidationAttribute(type);
                this.IsNeedValidate = (bool)ModelValidater._type[this.TypeName];
            }
            private void CachedValidationAttribute(Type type)
            {
                if (!ModelValidater._type.ContainsKey(this.TypeName))
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
                        string key = string.Format(ModelValidater._proFormat, this.TypeName, pro.Name);
                        Attribute[] array = Attribute.GetCustomAttributes(pro, typeValidationAttribute);
                        count += array.Length;
                        ModelValidater._proValidation[key] = array;
                    }
                    ModelValidater._type[this.TypeName] = (count > 0);
                }
            }
            public void Validate(PropertyDescriptor pd, object value)
            {
                if (this.IsNeedValidate)
                {
                    string key = string.Format(ModelValidater._proFormat, this.TypeName, pd.Name);
                    ValidationAttribute[] attrs = ModelValidater._proValidation[key] as ValidationAttribute[];
                    if (attrs == null || attrs.Length == 0)
                    {
                        return;
                    }
                    RequiredAttribute attRequired = attrs.FirstOrDefault((ValidationAttribute o) => o is RequiredAttribute) as RequiredAttribute;
                    if (attRequired != null && !attRequired.IsValid(value))
                    {
                        throw new Exception(attRequired.ErrorMessage);
                    }
                    ValidationAttribute[] array = attrs;
                    for (int i = 0; i < array.Length; i++)
                    {
                        ValidationAttribute attr = array[i];
                        if (!attr.IsValid(value))
                        {
                            throw new Exception(attr.ErrorMessage);
                        }
                    }
                }
            }
        }
    }
