using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MB.Code.Common.Http
{
    public sealed class HttpClient
    {
        public static string Get(string url, string httpPostType = "application/json;charset=utf-8", CookieContainer cookieContainer = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = httpPostType;
            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }
            string result;
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }
        public static async Task<string> GetAsync(string url, string httpPostType = "application/json;charset=utf-8", CookieContainer cookieContainer = null)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = httpPostType;
            if (cookieContainer != null)
            {
                httpWebRequest.CookieContainer = cookieContainer;
            }
            string result;
            using (WebResponse webResponse = await httpWebRequest.GetResponseAsync())
            {
                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }
        public static string Post(string url, string postData, string httpPostType = "application/json;charset=utf-8", CookieContainer cookieContainer = null)
        {
            byte[] buffer = (!string.IsNullOrEmpty(postData)) ? Encoding.UTF8.GetBytes(postData) : new byte[0];
            return HttpClient.Post(url, buffer, httpPostType, cookieContainer);
        }
        public static string Post(string url, byte[] postBuffer, string httpPostType = "application/json;charset=utf-8", CookieContainer cookieContainer = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = httpPostType;
            request.ContentLength = (long)postBuffer.Length;
            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }
            if (postBuffer.Length > 0)
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(postBuffer, 0, postBuffer.Length);
                }
            }
            string result;
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }
        public static async Task<string> PostAsync(string url, string postData, string httpPostType = "application/json;charset=utf-8", CookieContainer cookieContainer = null)
        {
            byte[] postBuffer = (!string.IsNullOrEmpty(postData)) ? Encoding.UTF8.GetBytes(postData) : new byte[0];
            return await HttpClient.PostAsync(url, postBuffer, httpPostType, cookieContainer);
        }
        public static async Task<string> PostAsync(string url, byte[] postBuffer, string httpPostType = "application/json;charset=utf-8", CookieContainer cookieContainer = null)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            new ManualResetEvent(false);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = httpPostType;
            httpWebRequest.ContentLength = (long)postBuffer.Length;
            if (cookieContainer != null)
            {
                httpWebRequest.CookieContainer = cookieContainer;
            }
            if (postBuffer.Length > 0)
            {
                using (Stream stream = await httpWebRequest.GetRequestStreamAsync())
                {
                    stream.Write(postBuffer, 0, postBuffer.Length);
                }
            }
            string result;
            using (WebResponse webResponse = await httpWebRequest.GetResponseAsync())
            {
                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }
    }
}


