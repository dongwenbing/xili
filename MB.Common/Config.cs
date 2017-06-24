using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;

namespace MB.Common
{
   public class weixinConfig
    {
        static public string appid = ConfigurationManager.AppSettings["appid"];
        static public string appsecret = ConfigurationManager.AppSettings["appsecret"];
        static public string domain = ConfigurationManager.AppSettings["domain"];



       
    }
}
