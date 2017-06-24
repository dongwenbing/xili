using System;
using System.Collections.Generic;
using System.Text;
using LitJson;


namespace MB.Common
{


    public class jssdk
    {
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();
        public jssdk()
        {

        }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string noncestr { get; set; }
        /// <summary>
        /// tickiet值
        /// </summary>
        public string jsapi_ticket { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string url { get; set; }
          /// <summary>
          /// 签名
          /// </summary>
        public string signature { get; set; }

        public string appid { get; set; }


        public string ToJson()
        {

            string jsonStr = JsonMapper.ToJson(m_values);
            return jsonStr;
        }

        public  string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    Logs.WirteLog(LogType.Trace, "进行签名有值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        public  string jsSdkMakeSign()
        {
            //转url排序格式
            string str = ToUrl();
            string Sign = strings.SHA1(str);
            return Sign;
        }
        //检测签名是否正确     正确返回true，错误抛异常
        public bool CheckSign()
        {

            if (!IsSet("sign"))
            {
                throw new Exception("WxPayData签名存在但不合法!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if (GetValue("sign") == null || GetValue("sign").ToString() == "")
            {

                throw new Exception("WxPayData签名存在但不合法!");
            }

            //获取接收到的签名
            string return_sign = GetValue("sign").ToString();

            //在本地计算新的签名
            string cal_sign = jsSdkMakeSign();

            if (cal_sign == return_sign)
            {
                return true;
            }
            throw new Exception("WxPayData签名验证错误!");
        }

        #region sjsdk类set   get 方法

        public void SetValue(string key, object value)
        {
            m_values[key] = value;
        }

        public object GetValue(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            return o;
        }


        public bool IsSet(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }

        public SortedDictionary<string, object> GetValues()
        {
            return m_values;
        }

        #endregion

    }
}
