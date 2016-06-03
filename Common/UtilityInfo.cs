using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Web.UI.WebControls;

namespace Common
{
    public class ResultEntity
    {
        public string SourceMethod { get; set; }
        public string TableName { get; set; }
        public string ColumnsName { get; set; }
        public string ColumnsValue { get; set; }
    }
    public class UtilityInfo
    {
        private static readonly Random MRand = new Random();
        /// <summary>
        /// 获得MD5加密后的字符串
        /// </summary>
        public static string Md5(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            if (b.Length > 0)
            {
                try
                {
                    var m = new MD5CryptoServiceProvider();
                    byte[] b2 = m.ComputeHash(b);
                    if (b2.Length > 0)
                    {
                        string ret = "";
                        for (int i = 0; i < b2.Length; i++)
                        {
                            ret += b2[i].ToString("x").PadLeft(2, '0');
                        }
                        return ret;
                    }
                }
                catch
                {
                    //nothing
                }
            }
            return string.Empty;
        }
        public static string MD5EncryptHash(String input)
        {
            byte[] result = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder output = new StringBuilder(16);
            for (int i = 0; i < result.Length; i++)
            {
                // convert from hexa-decimal to character  
                output.Append((result[i]).ToString("x2", System.Globalization.CultureInfo.InvariantCulture));
            }
            return output.ToString();
        }


        /// <summary>
        /// MD5摘要
        /// </summary>
        /// <param name="argInput">输入字符串</param>
        /// <param name="isUppder">返回是否大写</param>
        /// <returns>MD5哈希值</returns>
        public static string GetMd5Hash(string argInput, bool isUppder = false)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(argInput));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString(isUppder ? "X2" : "x2"));
            }
            return sBuilder.ToString();
        }
        public static string GetIp()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }
        /// <summary>
        ///   获取UA
        /// </summary>
        public static string GetUserAgent()
        {
            return string.IsNullOrEmpty(HttpContext.Current.Request.UserAgent) ? HttpContext.Current.Request.Headers["User-Agent"] : HttpContext.Current.Request.UserAgent;
        }
        public static string GetMobile()
        {
            string mobile = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["DEVICEID"]))
            {
                mobile = HttpContext.Current.Request.ServerVariables["DEVICEID"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_UP_SUBNO"]))
            {
                mobile = HttpContext.Current.Request.ServerVariables["HTTP_X_UP_SUBNO"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_NETWORK_INFO"]))
            {
                mobile = HttpContext.Current.Request.ServerVariables["HTTP_X_NETWORK_INFO"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_UP_CALLING_LINE_ID"]))
            {
                mobile = HttpContext.Current.Request.ServerVariables["HTTP_X_UP_CALLING_LINE_ID"];
            }
            if (!string.IsNullOrEmpty(mobile))
            {
                const string pattern = @"(1[358]\d{9})";
                MatchCollection mc = Regex.Matches(mobile, pattern);
                foreach (Match match in mc)
                {
                    mobile = match.ToString();
                }
            }
            return mobile;
        }

        public static void SaveCache(string key, object value, int expireSeconds)
        {
            HttpContext.Current.Cache.Add(key, value, null, DateTime.Now.AddSeconds(expireSeconds), TimeSpan.Zero, CacheItemPriority.High, null);
        }

        public static object GetCache(string key)
        {
            return HttpContext.Current.Cache.Get(key);
        }

        /// <summary>
        /// 是否为有效的邮件地址
        /// true 有效
        /// false 无效
        /// </summary>
        /// <param name="email">邮件地址</param>
        /// <returns>bool</returns>
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        /// <summary>
        /// 是否为有效的网站地址
        /// true 有效
        /// false 无效
        /// </summary>
        /// <param name="url">网址</param>
        /// <returns>bool</returns>
        /// 
        public static bool IsExistsUrl(string url)
        {
            return Regex.IsMatch(url, @"^(http(s)?:\/\/)?(www\.)?[\w-]+\.\w{2,4}(\/)?$");
        }

        /// <summary>
        /// 验证日期或日期时间有效性
        /// </summary>
        /// <param name="strDate">输入字符串</param>
        /// <returns></returns>
        public static bool IsDateTime(string strDate)
        {
            // 验证日期或日期时间有效性
            string regex = @"^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$";

            return Regex.IsMatch(strDate, regex);
        }

        /// <summary>
        /// 验证时间有效性
        /// </summary>
        /// <param name="strDate">输入字符串</param>
        /// <returns></returns>
        public static bool IsTime(string strDate)
        {
            // 验证时间有效性，正确格式如：10:00:00
            string regex = @"^((([0-1]?[0-9])|([2][0-3])):([0-5][0-9])(:[0-5][0-9])?)$";

            return Regex.IsMatch(strDate, regex);
        }

        /// <summary>
        /// int有效性
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsValidInt(string val)
        {
            return Regex.IsMatch(val, @"^-?[1-9]\d*|(^0$)$");
        }

        /// <summary>
        ///   生成验证码
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomText(int length)
        {
            const string so = "2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,M,N,Q,R,S,T,U,V,W,X,Y,Z";
            string[] strArr = so.Split(',');
            string code = "";
            for (int i = 0; i < length; i++)
            {
                code += strArr[MRand.Next(0, strArr.Length)];
            }
            return code;
        }
        public static string GetPasswordCode(int userId, DateTime createTime)
        {
            Random rd = new Random();
            int number = rd.Next(100001, 999999);
            string str = userId.ToString() + number.ToString() + createTime.ToString("yyyyMMdd");
            return Md5(str);
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return string.Empty;
            }

            return HttpContext.Current.Request.QueryString[strName].Trim();
        }

        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName)
        {
            return GetQueryInt(strName, 0);
        }

        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return defValue;
            }

            return Convert.ToInt32(HttpContext.Current.Request.QueryString[strName]);
        }

        /// <summary>
        /// 获取字符型参数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns>参数值（多个时，返回值之间以逗号","分隔）</returns>
        public static string GetStrParams(string paramName)
        {
            if (HttpContext.Current.Request.Params[paramName] == null)
            {
                return string.Empty;
            }

            return HttpContext.Current.Request.Params[paramName].ToString();
        }

        /// <summary>
        /// 获取整型参数值
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns>参数值</returns>
        public static int GetIntParams(string paramName)
        {
            return GetIntParams(paramName, 0);
        }

        /// <summary>
        /// 获取整型参数值
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        public static int GetIntParams(string paramName, int defaultValue)
        {
            if (HttpContext.Current.Request.Params[paramName] == null)
            {
                return defaultValue;
            }

            return Convert.ToInt32(HttpContext.Current.Request.Params[paramName]);
        }

        /// <summary>
        /// 绑定DropDownList
        /// </summary>
        /// <param name="dropDownList">DropDownList控件实例</param>
        /// <param name="datasource">数据源</param>
        /// <param name="textField">为各列表项提供文本内容的数据源字段</param>
        /// <param name="valueField">为各列表项提供值的数据源字段</param>
        /// <param name="selectedValue">默认的选择值</param>
        public static void BindDropDownList(DropDownList dropDownList, IEnumerable datasource, string textField, string valueField, string selectedValue)
        {
            BindDropDownList(dropDownList, datasource, textField, valueField, selectedValue, null);
        }

        /// <summary>
        /// 绑定DropDownList
        /// </summary>
        /// <param name="dropDownList">DropDownList控件实例</param>
        /// <param name="datasource">数据源</param>
        /// <param name="textField">为各列表项提供文本内容的数据源字段</param>
        /// <param name="valueField">为各列表项提供值的数据源字段</param>
        /// <param name="selectedValue">默认的选择值</param>
        /// <param name="topItem">添加到DropDownList的第一个元素</param>
        public static void BindDropDownList(DropDownList dropDownList, IEnumerable datasource, string textField, string valueField, string selectedValue, ListItem topItem)
        {
            if (valueField.Length < 1 || textField.Length < 1)
            {
                return;
            }
            if (datasource != null)
            {
                dropDownList.DataSource = datasource;
                dropDownList.DataTextField = textField;
                dropDownList.DataValueField = valueField;
                dropDownList.DataBind();
                if (topItem != null)
                {
                    dropDownList.Items.Insert(0, topItem);
                }
            }
            if (selectedValue != null && selectedValue.Length > 0)
            {
                if (dropDownList.Items.FindByValue(selectedValue) != null)
                {
                    dropDownList.SelectedValue = selectedValue;
                }
            }
        }

        /// <summary>
        /// 格式化字符串：隐藏字符串中间数据
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="keepCount">字符串尾保留的字符个数</param>
        /// <returns></returns>
        public static string FormatStringWithStar(string source, int keepCount)
        {
            string result = source;
            if (!string.IsNullOrEmpty(source) && source.Length >= keepCount)
            {
                result = "******" + source.Substring(source.Length - keepCount, keepCount);
            }
            return result;
        }

        /// <summary>
        /// zip压缩字符串
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ZipCompress(string content)
        {
            string retContent = "";
            byte[] contentBytes = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(content)));
            using (MemoryStream ms = new MemoryStream())
            {
                GZipStream Compress = new GZipStream(ms, CompressionMode.Compress);
                Compress.Write(contentBytes, 0, contentBytes.Length);
                Compress.Close();
                retContent = Convert.ToBase64String(ms.ToArray());
            }
            return retContent;
        }
        /// <summary>
        /// zip解压字符串
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ZipDeCompress(string content)
        {
            string retContent = "";
            byte[] contentBytes = Convert.FromBase64String(content);
            using (MemoryStream tempMs = new MemoryStream())
            {
                using (MemoryStream ms = new MemoryStream(contentBytes))
                {
                    GZipStream Decompress = new GZipStream(ms, CompressionMode.Decompress);
                    Decompress.CopyTo(tempMs);
                    Decompress.Close();
                    retContent = Encoding.UTF8.GetString(tempMs.ToArray());
                }
            }
            return retContent;
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static long ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (long)(time.Ticks - startTime.Ticks) / 10000;
        }

        /// <summary>
        /// 获取2011-12-22 23:59:59格式的时间
        /// </summary>
        /// <param name="endDate">时间</param>
        /// <returns>2011-12-22 23:59:59格式的时间</returns>
        public static DateTime GetTheLastSecondDateTime(DateTime endDate)
        {
            return endDate.Date.AddDays(1).AddSeconds(-1);
        }

        public static List<string> GetSubStringList(string str, char sepeater)
        {
            var ss = str.Split(sepeater);
            return ss.Where(s => !string.IsNullOrEmpty(s) && s != sepeater.ToString(CultureInfo.InvariantCulture)).ToList();
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDbc(string input)
        {
            var c = input.ToCharArray();
            for (var i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                {
                    c[i] = (char)(c[i] - 65248);
                }
            }
            return new string(c);
        }

        //public static void SendResultEntityMQ(SiteName siteName,string sourceMethod, string tableName,string columnsName,string columnsValue)
        //{
        //    var resultEntity=new ResultEntity
        //    {
        //        SourceMethod=sourceMethod,
        //        ColumnsName = columnsName,
        //        ColumnsValue = columnsValue,
        //        TableName = tableName
        //    };
        //    try
        //    {
        //        MessageQueueMgr.SendMessage(resultEntity, Config.ResultEntityPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        var lr = new LogMqRecord(siteName, LogLeve.Error, "SendResultEntityMQ")
        //        {
        //            ParasNameValue = string.Format("tableName={0}&columnsName={1}&columnsValue={2}", tableName,
        //                columnsName, columnsValue),
        //            ExtMessage = string.Format("{0}", ex),
        //            Status = 0
        //        };
        //        LogHelper.WriteLogMq(lr);
        //    }
        //}

        public static string GetHideString(string phone)
        {
            string temp = "";
            if (phone.Length > 7)
            {
                for (int i = 0; i < phone.Length; i++)
                {
                    if (i > 2 && i < 7)
                    {
                        temp += "*";
                    }
                    else
                        temp += phone[i];
                }
            }
            return temp;
        }

        public static string InputText(string text, int maxLength)
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);
            text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = text.Replace("'", "''");

            return text;
        }
    }
}
