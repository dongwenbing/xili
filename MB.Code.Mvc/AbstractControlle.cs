using System;
using System.Web.Mvc;
using System.Web.SessionState;
using MB.Code.Common;


namespace MB.Code.Mvc
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public abstract class AbstractController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            Error err;
            if (ex is BusinessException)
            {
                BusinessException busException = ex as BusinessException;
                err = busException.Error;
            }
            else
            {
                err = new Error();
                err.Message = ex.Message;
                err.Detail = ex.ToString();
            }
            filterContext.Result=this.Return<object>(new Result<object>
            {
                Error = err,
                Status = ResultStatus.Error
            }, 0);
            filterContext.ExceptionHandled=true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
        public ActionResult Return<T>(Result<T> result)
        {
            return new JsonExtendResult(result, 0);
        }
        public ActionResult Return<T>(Result<T> result, JsonRequestBehavior behavior)
        {
            return new JsonExtendResult(result, behavior);
        }
        public ActionResult Return<T>(Result<T> result, JsonRequestBehavior behavior, string dateFmt)
        {
            return new JsonExtendResult(result, behavior, dateFmt);
        }
        public ActionResult Return<T>(Result<T> result, JsonRequestBehavior behavior, int decimalPlaces)
        {
            return new JsonExtendResult(result, behavior, decimalPlaces);
        }
        public ActionResult Return<T>(Result<T> result, JsonRequestBehavior behavior, string dateFmt, int decimalPlaces)
        {
            return new JsonExtendResult(result, behavior, dateFmt, decimalPlaces);
        }
        protected ActionResult Redirect(string actionName, string controllerName)
        {
            return base.RedirectToRoute(new
            {
                controller = controllerName,
                action = actionName
            });
        }
        protected ActionResult OK()
        {
            return this.OK(null);
        }
        protected ActionResult OK(object data)
        {
            return this.GetResultWithSuccess(data, 0, 0, "yyyy-MM-dd HH:mm:ss", 2);
        }
        protected ActionResult OK(object data, JsonRequestBehavior behavior)
        {
            return this.GetResultWithSuccess(data, 0, behavior, "yyyy-MM-dd HH:mm:ss", 2);
        }
        protected ActionResult OK(object data, int totalCount, JsonRequestBehavior behavior)
        {
            return this.GetResultWithSuccess(data, totalCount, behavior, "yyyy-MM-dd HH:mm:ss", 2);
        }
        protected ActionResult OK(object data, int totalCount, JsonRequestBehavior behavior, string dateFmt)
        {
            return this.GetResultWithSuccess(data, totalCount, behavior, dateFmt, 2);
        }
        protected ActionResult OK(object data, int totalCount, JsonRequestBehavior behavior, int decimalPlaces)
        {
            return this.GetResultWithSuccess(data, totalCount, behavior, "yyyy-MM-dd HH:mm:ss", decimalPlaces);
        }
        protected ActionResult OK(object data, int totalCount, JsonRequestBehavior behavior, string dateFmt, int decimalPlaces)
        {
            return this.GetResultWithSuccess(data, totalCount, behavior, dateFmt, decimalPlaces);
        }
        private ActionResult GetResultWithSuccess(object data, int totalCount, JsonRequestBehavior behavior, string dateFmt = "yyyy-MM-dd HH:mm:ss", int decimalPlaces = 2)
        {
            Result<object> result = this.GetResultWithSuccess(data, totalCount);
            return this.Return<object>(result, behavior, dateFmt, decimalPlaces);
        }
        private Result<object> GetResultWithSuccess(object data, int totalCount = 0)
        {
            return new Result<object>
            {
                Status = ResultStatus.Success,
                TotalCount = totalCount,
                Data = data
            };
        }
    }
}
