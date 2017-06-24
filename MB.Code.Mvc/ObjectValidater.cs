using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MB.Code.Mvc
{
    public class ObjectValidater
    {
        private ValidationAttribute[] Validations
        {
            get;
            set;
        }
        public PropertyDescriptor Property
        {
            get;
            private set;
        }
        public ObjectValidater(ValidationAttribute[] validations, PropertyDescriptor property)
        {
            this.Validations = validations;
            this.Property = property;
        }
        public void Validate(object value)
        {
            if (this.Validations == null || this.Validations.Length == 0)
            {
                return;
            }
            RequiredAttribute attRequired = this.Validations.FirstOrDefault((ValidationAttribute o) => o is RequiredAttribute) as RequiredAttribute;
            if (attRequired != null && !attRequired.IsValid(value))
            {
                throw new Exception(attRequired.ErrorMessage);
            }
            ValidationAttribute[] validations = this.Validations;
            for (int i = 0; i < validations.Length; i++)
            {
                ValidationAttribute attr = validations[i];
                if (!attr.IsValid(value))
                {
                    throw new Exception(attr.ErrorMessage);
                }
            }
        }
    }
}
