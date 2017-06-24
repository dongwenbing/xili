
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace MB.Code.Common.Security.Cryptography
{
    public sealed class DES
    {
        public static byte[] Encrypt(string encryptString, string sKey, string sIV, Encoding encoding = null)
        {
            if (sKey.Length != 8 || sIV.Length != 8)
            {
                throw new ApplicationException();
            }
            encoding = (encoding ?? Encoding.UTF8);
            byte[] result;
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider
            {
                Key = encoding.GetBytes(sKey),
                IV = encoding.GetBytes(sIV)
            })
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] buffer = encoding.GetBytes(encryptString);
                        cs.Write(buffer, 0, buffer.Length);
                        cs.FlushFinalBlock();
                        result = ms.ToArray();
                    }
                }
            }
            return result;
        }
        public static string Decrypt(byte[] decryptBuffer, string sKey, string sIV, Encoding encoding = null)
        {
            if (sKey.Length != 8 || sIV.Length != 8)
            {
                throw new ApplicationException();
            }
            encoding = (encoding ?? Encoding.UTF8);
            string result;
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider
            {
                Key = encoding.GetBytes(sKey),
                IV = encoding.GetBytes(sIV)
            })
            {
                using (MemoryStream ms = new MemoryStream(decryptBuffer))
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs, encoding))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                }
            }
            return result;
        }
    }
}

