using System.Web.Mvc;
using MB.Common.MVC.Base;
using MB.Common;


namespace MB.Controlles.Home
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            string wen=  MB.Services.Home.gare.getdata();
            Logs.WirteLog(LogType.Trace, wen);

            wen = weixin.Token();
            Logs.WirteLog(LogType.Trace, wen);
            ViewBag.tt = wen;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            string gg = "";

            //调试来着
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult WeixinSdk()
        {




            return View();
        }

    }
}
