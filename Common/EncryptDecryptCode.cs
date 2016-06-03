using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Common
{
    public class EncryptDecryptCode
    {
        public static string GetStr(string str)
        {
            string str2 = "*[";
            string str3 = "]";
            string str4 = str;
            if (!String.IsNullOrEmpty(str4))
            {
                int index = str4.IndexOf(str2);
                for (int i = str4.IndexOf(str3); (index > -1) && (i > index); i = str4.IndexOf(str3))
                {
                    string str5 = str4.Substring(0, index);
                    string str6 = str4.Substring(i + str3.Length);
                    int startIndex = index + str2.Length;
                    string name = str4.Substring(startIndex, i - startIndex);
                    string str8 = DecryptDES(name);
                    if (str8 == null)
                    {
                        str4 = str5 + name + str6;
                    }
                    else
                    {
                        str4 = str5 + str8 + str6;
                    }
                    index = str4.IndexOf(str2);
                }
            }
            return str4;
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="utf8Str">原文(UTF8编码)</param>
        /// <param name="keyBase64Str">例如：olJ/+Jp8Vko=
        /// 生成非弱密钥方法：new DESCryptoServiceProvider().GenerateKey()</param>
        /// <param name="iVBase64Str">例如：3l6c5u4IlDg=
        /// 生成向量方法：new DESCryptoServiceProvider().GenerateIV()</param>
        /// <returns>密文(base64编码)</returns>
        public static string EncryptDES(string utf8Str, string keyBase64Str = "olJ/+Jp8Vko=", string iVBase64Str = "3l6c5u4IlDg=")
        {
            var key = Convert.FromBase64String(keyBase64Str);
            var iv = Convert.FromBase64String(iVBase64Str);
            byte[] ret;
            using (var ms = new MemoryStream())
            {
                SymmetricAlgorithm mCSP = new DESCryptoServiceProvider();//new TripleDESCryptoServiceProvider();
                var ct = mCSP.CreateEncryptor(key, iv);

                using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                {
                    var byt = Encoding.UTF8.GetBytes(utf8Str);
                    cs.Write(byt, 0, byt.Length);
                    cs.FlushFinalBlock();
                }
                ret = ms.ToArray();
            }
            return Convert.ToBase64String(ret);
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="base64Str">密文(base64编码)</param>
        /// <param name="keyBase64Str">例如：olJ/+Jp8Vko=
        /// 生成非弱密钥方法：new DESCryptoServiceProvider().GenerateKey()</param>
        /// <param name="iVBase64Str">例如：3l6c5u4IlDg=
        /// 生成向量方法：new DESCryptoServiceProvider().GenerateIV()</param>
        /// <returns>原文(UTF8编码)</returns>
        public static string DecryptDES(string base64Str, string keyBase64Str = "olJ/+Jp8Vko=", string iVBase64Str = "3l6c5u4IlDg=")
        {
            var key = Convert.FromBase64String(keyBase64Str);
            var iv = Convert.FromBase64String(iVBase64Str);
            byte[] ret;
            using (var ms = new MemoryStream())
            {
                SymmetricAlgorithm mCSP = new DESCryptoServiceProvider();//new TripleDESCryptoServiceProvider();
                var ct = mCSP.CreateDecryptor(key, iv);
                using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                {
                    byte[] byt = Convert.FromBase64String(base64Str);
                    cs.Write(byt, 0, byt.Length);
                    cs.FlushFinalBlock();
                }
                ret = ms.ToArray();
            }
            return Encoding.UTF8.GetString(ret);
        }


        /// <summary>
        /// 3des加密字符串
        /// </summary>
        /// <param name="utf8Str">原文(UTF8编码)</param>
        /// <param name="keyBase64Str">例如：olJ/+Jp8Vko=
        /// 生成非弱密钥方法：new TripleDESCryptoServiceProvider().GenerateKey()</param>
        /// <param name="iVBase64Str">例如：3l6c5u4IlDg=
        /// 生成向量方法：new TripleDESCryptoServiceProvider().GenerateIV()</param>
        /// <returns>密文(base64编码)</returns>
        public static string Encrypt3DES(string utf8Str, string keyBase64Str = "0nAo+6t79mu6iLF1mxF2jkeNZpGgYA58", string iVBase64Str = "LAGq0oBZUnw=")
        {
            var key = Convert.FromBase64String(keyBase64Str);
            var iv = Convert.FromBase64String(iVBase64Str);
            ICryptoTransform desEncrypt;
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;//官方建议：ECB 存在多个安全隐患,易被破解
                desEncrypt = des.CreateEncryptor(key, iv);
            }

            byte[] buffer = Encoding.UTF8.GetBytes(utf8Str);
            return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }

        /// <summary>
        /// 3des解密字符串
        /// </summary>
        /// <param name="base64Str">密文(base64编码)</param>
        /// <param name="keyBase64Str">例如：olJ/+Jp8Vko=
        /// 生成非弱密钥方法：new TripleDESCryptoServiceProvider().GenerateKey()</param>
        /// <param name="iVBase64Str">例如：3l6c5u4IlDg=
        /// 生成向量方法：new TripleDESCryptoServiceProvider().GenerateIV()</param>
        /// <returns>原文(UTF8编码)</returns>
        public static string Decrypt3DES(string base64Str, string keyBase64Str = "0nAo+6t79mu6iLF1mxF2jkeNZpGgYA58", string iVBase64Str = "LAGq0oBZUnw=")
        {
            var key = Convert.FromBase64String(keyBase64Str);
            var iv = Convert.FromBase64String(iVBase64Str);
            ICryptoTransform desEncrypt;
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;//官方建议：ECB 存在多个安全隐患,易被破解
                desEncrypt = des.CreateDecryptor(key, iv);
            }

            byte[] buffer = Convert.FromBase64String(base64Str);
            return Encoding.UTF8.GetString(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }
    }
}
