using System;
using System.Web.Mvc;

namespace MB.Code.Mvc
{
    public class CustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            return new CustomModelBinder();
        }
    }
}
