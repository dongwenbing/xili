using System;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Security;    


namespace MB.Common
{
    /// <summary>
    /// http连接基础类，负责底层的http通信
    /// </summary>
    public class HttpService
    {

        public static bool CheckValidationResult(object sender, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        public static string Post(string json, string url, int timeout)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
            string result = "";//返回结果
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;
            try
            {
                #region try
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 1000;
                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = timeout * 1000;
                //设置代理服务器
                // WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = "text/xml";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
                request.ContentLength = data.Length;

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();
                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close(); 
                #endregion

            }
            catch (System.Threading.ThreadAbortException e)
            {
                Logs.WirteLog(LogType.Exception, "ThreadAbortException : " + e.ToString());
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Logs.WirteLog(LogType.Exception, "StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    Logs.WirteLog(LogType.Exception, "StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw new Exception(e.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }

        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static string Get(string url)
        {
            System.GC.Collect();
            string result = "";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                //设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();
                //获取HTTP返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                Logs.WirteLog(LogType.Exception, "ThreadAbortException : " + e.ToString());
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Logs.WirteLog(LogType.Exception, "StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    Logs.WirteLog(LogType.Exception, "StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw new Exception(e.ToString());
            }
            catch (Exception e)
            {
                Logs.WirteLog(LogType.Exception, "HttpService : "+ e.ToString());
                throw new Exception(e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }



        public static string newPost(string postxml,string url)
        { 
             WebClient wc = new WebClient();
            byte[] postData = Encoding.UTF8.GetBytes(postxml);//编码，尤其是汉字，事先要看下抓取网页的编码方式 

           // postData = Encoding.UTF8.GetBytes("wenbing");
         
            string srcString = "";
            try
            {
                byte[] responseData = wc.UploadData(url, "POST", postData);//得到返回字符流

                srcString = Encoding.UTF8.GetString(responseData);//解码
               
            }
            catch
            {
              
            }

            return srcString;
        
        }
    }
}