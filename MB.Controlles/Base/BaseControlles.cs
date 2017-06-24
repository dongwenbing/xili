using MB.Code.Mvc;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using MB.Services;
using MB.Common;

namespace MB.Controlles.Base
{
    public class BaseController : AbstractController
    {
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        //此处后期计划添加  登录，注销判断等方法

        public string BackJson(object obj)
        {

            return "";
        }


 

    }
}
