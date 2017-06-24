using System;
using System.Web.Mvc;


namespace MB.Code.Mvc
{
    public class CustomModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (!TypeResolver.IsExtendValueType(bindingContext.ModelType))
            {
                result = (result ?? bindingContext.ValueProvider.GetValue(string.Empty));
            }
            if (result != null)
            {
                return result.ConvertTo(bindingContext.ModelType);
            }
            return null;
        }
    }
}
