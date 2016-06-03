using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    /// <summary>
    ///     该类用于与AOF和其他系统对接的加解密算法，虽然有问题，但不能轻易修改
    /// </summary>
    public static class DesCodeForAfoAndApp
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="strDefaultKey"></param>
        /// <returns></returns>
        public static string DESEnCodeForAfoAndApp(this string pToEncrypt, string strDefaultKey = "L6Xe8dxv")
        {
            var k = Encoding.ASCII.GetBytes(strDefaultKey.Substring(0, 8));
            var des = new DESCryptoServiceProvider
            {
                Key = k,
                IV = k,
                Mode = CipherMode.ECB
            };

            StringBuilder ret;
            var inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();

                    ret = new StringBuilder();
                    foreach (var b in ms.ToArray())
                    {
                        ret.AppendFormat("{0:x2}", b);
                    }
                }
            }
            des.Dispose();
            return ret.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="isErrorOutPutSource"></param>
        /// <param name="strDefaultKey"></param>
        /// <returns></returns>
        public static string DESDeCodeForAfoAndApp(this string pToDecrypt, bool isErrorOutPutSource = true, string strDefaultKey = "L6Xe8dxv")
        {
            if (string.IsNullOrEmpty(pToDecrypt))
                return string.Empty;
            var inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < inputByteArray.Length; x++)
            {
                inputByteArray[x] = Convert.ToByte(pToDecrypt.Substring(x * 2, 2), 16);
            }
            var k = Encoding.ASCII.GetBytes(strDefaultKey.Substring(0, 8));
            var des = new DESCryptoServiceProvider
            {
                Key = k,
                IV = k,
                Mode = CipherMode.ECB
            };

            var str = string.Empty;
            try
            {
                using (var ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }

                    str = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                str = isErrorOutPutSource ? pToDecrypt : string.Empty;
            }
            des.Dispose();
            return str;
        }
    }
}