

    using System;
    using System.Security.Cryptography;
    using System.Text;
    namespace MB.Code.Common.Security.Cryptography
{
        public sealed class RSA
        {
            private static readonly CspParameters cspParameters = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            public static void RSAKey(out string xmlKeys, out string xmlPublicKey)
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(RSA.cspParameters))
                {
                    xmlKeys = rsa.ToXmlString(true);
                    xmlPublicKey = rsa.ToXmlString(false);
                }
            }
            public static string RSAEncrypt(string xmlPublicKey, string encryptString, Encoding encoding = null)
            {
                encoding = (encoding ?? Encoding.UTF8);
                byte[] buffer = RSA.RSAEncrypt(xmlPublicKey, encoding.GetBytes(encryptString));
                return Convert.ToBase64String(buffer);
            }
            public static byte[] RSAEncrypt(string xmlPublicKey, byte[] encryptBuffer)
            {
                byte[] result;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(RSA.cspParameters))
                {
                    rsa.FromXmlString(xmlPublicKey);
                    result = rsa.Encrypt(encryptBuffer, false);
                }
                return result;
            }
            public static string RSADecrypt(string xmlPrivateKey, string decryptString, Encoding encoding = null)
            {
                byte[] buffer = RSA.RSADecrypt(xmlPrivateKey, Convert.FromBase64String(decryptString));
                return (encoding ?? Encoding.UTF8).GetString(buffer);
            }
            public static byte[] RSADecrypt(string xmlPrivateKey, byte[] decryptbuffer)
            {
                byte[] result;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(RSA.cspParameters))
                {
                    rsa.FromXmlString(xmlPrivateKey);
                    result = rsa.Decrypt(decryptbuffer, false);
                }
                return result;
            }
            public static byte[] CreateSignature(string xmlPrivateKey, byte[] encryptBuffer)
            {
                byte[] result;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(xmlPrivateKey);
                    RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                    rsaFormatter.SetHashAlgorithm("MD5");
                    result = rsaFormatter.CreateSignature(MD5.Create(encryptBuffer));
                }
                return result;
            }
            public static string CreateSignature(string xmlPrivateKey, string encryptString, Encoding encoding = null)
            {
                encoding = (encoding ?? Encoding.UTF8);
                return Convert.ToBase64String(RSA.CreateSignature(xmlPrivateKey, encoding.GetBytes(encryptString)));
            }
            public static bool VerifySignature(string xmlPublicKey, byte[] encryptBuffer, byte[] signatureBuffer)
            {
                bool result;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(xmlPublicKey);
                    RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                    RSADeformatter.SetHashAlgorithm("MD5");
                    result = RSADeformatter.VerifySignature(MD5.Create(encryptBuffer), signatureBuffer);
                }
                return result;
            }
            public static bool VerifySignature(string xmlPublicKey, string encryptString, string signatureString, Encoding encoding = null)
            {
                encoding = (encoding ?? Encoding.UTF8);
                return RSA.VerifySignature(xmlPublicKey, encoding.GetBytes(encryptString), Convert.FromBase64String(signatureString));
            }
        }
    }


