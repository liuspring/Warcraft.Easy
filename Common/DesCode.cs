using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    //C# 版DES 加解密算法
    public static class DesCode
    {
        //加解密密钥
        private static string sKey = "LsdfLJasdf443OUoDfiFosd";//默认密钥
        #region DESEnCode DES加密

        /// <summary>
        /// 加密（返回16进制字符串）
        /// </summary>
        /// <param name="pToEncrypt">int类型进行加密</param>
        /// <returns></returns>
        public static string DESEnCode(this int pToEncrypt)
        {
            return pToEncrypt.ToString().DESEnCode();
        }

        /// <summary>
        /// 加密(返回16进制字符串)
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <returns></returns>
        public static string DESEnCode(this string pToEncrypt)
        {
            if (String.IsNullOrEmpty(pToEncrypt))
                return String.Empty;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);
            //建立加密对象的密钥和偏移量    
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法    
            //使得输入密码必须输入英文文本
            des.Key = Encoding.ASCII.GetBytes(sKey.Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(sKey.Substring(sKey.Length - 8, 8));
            des.Mode = CipherMode.CBC;//官方建议：ECB 存在多个安全隐患,易被破解 https://msdn.microsoft.com/library/system.security.cryptography.ciphermode(v=vs.100).aspx
            StringBuilder ret;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();

                }
                ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:x2}", b);//输出为16进制
                }
            }
            des.Dispose();

            return ret.ToString();
        }

        #endregion

        #region DESDeCode DES解密

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="pToDecrypt"> 待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DESDeCode(this string pToDecrypt)
        {
            if (String.IsNullOrEmpty(pToDecrypt) || pToDecrypt.Length % 2 > 0)
                return String.Empty;
            //由于采用16进制，导致最后一奇数位被忽略，所以添加了“取余”限制法则
            string str = String.Empty;
            try
            {
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < inputByteArray.Length; x++)
                {
                    inputByteArray[x] = Convert.ToByte(pToDecrypt.Substring(x * 2, 2), 16);
                }
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = Encoding.ASCII.GetBytes(sKey.Substring(0, 8));
                    des.IV = Encoding.ASCII.GetBytes(sKey.Substring(sKey.Length - 8, 8));
                    des.Mode = CipherMode.CBC;//官方建议：ECB 存在多个安全隐患,易被破解 https://msdn.microsoft.com/library/system.security.cryptography.ciphermode(v=vs.100).aspx
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                        }
                        str = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch { }

            return str;
        }

        #endregion

        /// <summary>
        /// 获取解密字符串 DesCode.GetStr("*[VU7rREEvLYc=]")值等于 2
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetStr(this string str)
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
        public static string EncryptDES(this string utf8Str, string keyBase64Str = "olJ/+Jp8Vko=", string iVBase64Str = "3l6c5u4IlDg=")
        {
            var key = Convert.FromBase64String(keyBase64Str);
            var iv = Convert.FromBase64String(iVBase64Str);
            byte[] ret;
            using (var ms = new MemoryStream())
            {
                SymmetricAlgorithm mCsp = new DESCryptoServiceProvider();//new TripleDESCryptoServiceProvider();
                mCsp.Mode = CipherMode.CBC;//官方建议：ECB 存在多个安全隐患,易被破解
                var ct = mCsp.CreateEncryptor(key, iv);

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
        public static string DecryptDES(this string base64Str, string keyBase64Str = "olJ/+Jp8Vko=", string iVBase64Str = "3l6c5u4IlDg=")
        {
            var key = Convert.FromBase64String(keyBase64Str);
            var iv = Convert.FromBase64String(iVBase64Str);
            byte[] ret;
            using (var ms = new MemoryStream())
            {
                SymmetricAlgorithm mCsp = new DESCryptoServiceProvider();//new TripleDESCryptoServiceProvider();
                mCsp.Mode = CipherMode.CBC;//官方建议：ECB 存在多个安全隐患,易被破解
                var ct = mCsp.CreateDecryptor(key, iv);
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
        public static string Encrypt3DES(this string utf8Str, string keyBase64Str = "0333dfgnAo+sadfsdsd", string iVBase64Str = "L34AGqs0oBZUnw=")
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
        public static string Decrypt3DES(this string base64Str, string keyBase64Str = "0333dfgnAo+sadfsdsd", string iVBase64Str = "L34AGqs0oBZUnw=")
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
