using System.Web.Mvc;
using MB.Common.MVC.Base;
using MB.Common;
using Newtonsoft.Json;



namespace MB.Controlles.Weixin
{
    public class WeixinController : BaseController
    {
        public ActionResult Demo()
        {
            //   string wen = MB.Services.Home.gare.getdata();
            string wen = string.Empty;
 
            ViewBag.tt = wen;
        //    Logs.WirteLog(LogType.Trace, wen);
            return View();
        }


        public string WeixinSdk()
        {
            //string ticket = HttpService.Get("http://www.dfmeibao.com/product2/classifylist.jhtml");
            jssdk sdk = weixin.GetJssdk("/Weixin/Demo");
            string result = sdk.ToJson();
            //Logs.WirteLog(LogType.Trace, wen);
            return result;
        }

        public string UserInfo()
        {
            string code = Request["code"];
            string state = Request["state"];
            if (!string.IsNullOrEmpty(code))
            {
                string openidstr=weixin.OpenidAndAccesstoken(code);
                MB.Services.Models.WUserBase userbase = JsonConvert.DeserializeObject<MB.Services.Models.WUserBase>(openidstr);//反序列化成WUserBase实体类
                string userinfo = weixin.UserInfo(userbase.access_token,userbase.openid);

                Logs.WirteLog(LogType.Trace, "获取到的用户信息：" + userinfo);

            }
            else
            {
                string  codeurl=weixin.CodeUrl("/Weixin/UserInfo");//获取code，页面将在回调时拿到code,参数为回调接收页面
                Response.Redirect(codeurl);
            }
            Logs.WirteLog(LogType.Trace, "获取到的code："+code+"state:"+state);

           
            return  "返回code："+code;

        }


    }
}
