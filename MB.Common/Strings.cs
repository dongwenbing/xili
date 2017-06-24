using System;
using System.Text.RegularExpressions;
using System.Text;



namespace MB.Common
{
    public  class strings
    {

        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }

        /// <summary>
        /// 截取两个字符串之间的 字符串
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="headstr"></param>
        /// <param name="endstr"></param>
        /// <returns></returns>
        public static string Stringcut(string str1, string headstr, string endstr)
        {
            MatchCollection mc;
            Regex r = new Regex(@"(?<=" + headstr + ").*?(?=" + endstr + ")", RegexOptions.Singleline | RegexOptions.IgnoreCase); //定义一个Regex对象实例
            mc = r.Matches(str1);
            if (mc.Count > 0) return (mc[0].Value); else { return (""); }
        }

        public static string SHA1(string text)
        {
            byte[] cleanBytes = Encoding.Default.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }


        static public string Gettimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }


    }
}
