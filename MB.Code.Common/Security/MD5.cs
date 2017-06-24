using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MB.Code.Common.Security.Cryptography
{
    public sealed class MD5
    {

        public static byte[] Create(byte[] buffer)
        {
            byte[] result;
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                result = md5.ComputeHash(buffer);
            }
            return result;
        }
        public static string Create(Stream inputStream)
        {
            string result;
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                result = string.Concat(
                    from h in md5.ComputeHash(inputStream)
                    select h.ToString("x"));
            }
            return result;
        }
        public static string Create(string stringCode, Encoding encoding = null)
        {
            StringBuilder result = new StringBuilder();
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            encoding = (encoding ?? Encoding.UTF8);
            byte[] s = md5.ComputeHash(encoding.GetBytes(stringCode));
            for (int i = 0; i < s.Length; i++)
            {
                result.Append(s[i].ToString("x2"));
            }
            return result.ToString();
        }
    }
}


