using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Web.Mvc;



namespace MB.Common
{
    public class weixin
    {
     
        public weixin()
        {

        }


        static public string UserInfo(string accesstoken,string openid)
        {

            Logs.WirteLog(LogType.Trace, "获取到的用户信息：accesstoken" + accesstoken +"opneid"+ openid);
            string userinfo = getuserinfo(accesstoken,openid);
            return userinfo;    
        }

        static public string  OpenidAndAccesstoken(string code)
        {
            string openidstr = getopenidandaccesstoken(code);
            return openidstr;
        }


        static public string CodeUrl(string action)
        {
            //snsapi_userinfo   snsapi_base
            string redirecturl = "http://" + weixinConfig.domain + action;
            redirecturl = strings.UrlEncode(redirecturl);
            string codeurl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + weixinConfig.appid + "&redirect_uri=" + redirecturl + "&response_type=code&scope=snsapi_base&state=wenbing#wechat_redirect";
            return codeurl;
        }

        static public jssdk GetJssdk(string path)
        {
            string url = "http://" + weixinConfig.domain + path;

            jssdk sdk = new jssdk();
            sdk.SetValue("noncestr",strings.GenerateNonceStr());
            sdk.SetValue("jsapi_ticket", Ticket());
            sdk.SetValue("timestamp", strings.Gettimestamp());
            sdk.SetValue("url", url);
            sdk.SetValue("signature",sdk.jsSdkMakeSign());
            sdk.SetValue("appid", weixinConfig.appid);
            return sdk;
        }


        static public string Token()
        {
            string token = redorwrite("token");
            return token;
        }

        static public string Ticket()
        {
            string ticket = redorwrite("ticket");
            return ticket;

        }

        /// <summary>
        /// 读取，更新token或者ticket的值。
        /// </summary>
        /// <param name="filename">传入token或者ticket</param>
        /// <returns></returns>
        static string redorwrite(string filename)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Doc\" + filename + ".txt";//文件地址
            string returnvlue = "";
            string vlueold = "";
            string nowtime = strings.Gettimestamp();
            string timevlue = "";
            if (File.Exists(path))//如果文件存在
            {
                string line = "";
                StreamReader srtxt = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
                while ((line = srtxt.ReadLine()) != null && line.ToString() != "")
                {
                    timevlue = line.ToString().Split('$')[0];
                    vlueold = line.ToString().Split('$')[1];
                }
                srtxt.Close();

                int re = int.Parse(nowtime) - int.Parse(timevlue);
                if (re > 7100)
                {

                    string newvlue = string.Empty;
                    if (filename == "token")
                    {
                        newvlue = strings.Stringcut(gettoken(), "access_token\":\"", "\",\"expires_in");
                    }
                    if (filename == "ticket")
                    {
                        newvlue = strings.Stringcut(getjsapi_ticket(), "ticket\":\"", "\",\"expires_in");

                    }
                    if (!string.IsNullOrEmpty(newvlue))
                    {
                        FileStream aFile = new FileStream(path, FileMode.Create);
                        StreamWriter sw = new StreamWriter(aFile, Encoding.GetEncoding("UTF-8"));
                        sw.Write(nowtime + "$" + newvlue);//写入最新值
                        sw.Close();
                        aFile.Close();
                        returnvlue = newvlue;
                        Logs.WirteLog(LogType.Trace, filename + "：时间大于7200，获取新值为：" + newvlue);
                    }
                    else {
                        againwrite(filename);//微信接口获取值有问题，初始化缓存文件内容
                    }

                }
                else//在3个小时以内，返回原有ticket值
                {
                    if (string.IsNullOrEmpty(vlueold))
                    {
                        againwrite(filename);
                    }
                    returnvlue = vlueold;
                }
            }
            return returnvlue;

        }

        static void againwrite(string filename)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Doc\" + filename + ".txt";//文件地址
            FileStream aFile = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(aFile, Encoding.GetEncoding("UTF-8"));
            sw.Write("1111111111$IWUqAcgx2FKx80k_MQij7pzk1P2YIe0y7XG-MdGBVruV0g6gBsDEnfF0Jxa0ULQTONTEyFjUjVejl9_tKLonN_T3Nrwbl9y9kSA5WiDsdf4MNRbABAUVZ");//重新，初始化值，
            sw.Close();
            aFile.Close();
            Logs.WirteLog(LogType.Trace, filename + "的值获取为空，现在已做初始化处理");
        }


        #region 腾讯微信接口

        private static string getuserinfo(string accesstoken,string openid)
        {
               
            string userinfourl = "https://api.weixin.qq.com/sns/userinfo?access_token=" + accesstoken + "&openid=" + openid + "&lang=zh_CN";
            string openidstr = HttpService.Get(userinfourl);
            return openidstr;

          
        }

        private static string getopenidandaccesstoken(string code)
        {
            string openiduel = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + weixinConfig.appid + "&secret=" + weixinConfig.appsecret + "&code=" + code + "&grant_type=authorization_code";
            string openidstr = HttpService.Get(openiduel);
            return openidstr;
        }


        private static string getuserinfoaccess_token()
        {
            string accesstokenurl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + weixinConfig.appid + "&redirect_uri=REDIRECT_URI&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect";
            return  "";
        }

        private static string gettoken()
        {
            //获取token值url
            string tokenurl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + weixinConfig.appid + "&secret=" + weixinConfig.appsecret;
            string token = HttpService.Get(tokenurl);
            Logs.WirteLog(LogType.Trace, "从微信接口拉取token值：" + token);
            return token;
        }
        private static string getjsapi_ticket()
        {
            //获取token值url
            string ticketurl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + Token() + "&type=jsapi";
            string ticket = HttpService.Get(ticketurl);
            Logs.WirteLog(LogType.Trace, "从微信接口拉取ticket值：" + ticket);
            return ticket;
        }
        #endregion
    }
}
