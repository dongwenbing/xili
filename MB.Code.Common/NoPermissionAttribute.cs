using System;


namespace MB.Code.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoPermissionAttribute : Attribute
    {
    }
}
