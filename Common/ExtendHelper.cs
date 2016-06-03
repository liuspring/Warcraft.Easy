using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Common
{
    public static class ExtendHelper
    {
        public static List<T> DataTableToList<T>(this DataTable dt) where T : new()
        {
            // 定义集合
            List<T> list = new List<T>();
            // 获得此模型的类型
            Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    // 检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                    {

                        // 判断此属性是否有Setter
                        if (!pi.CanWrite)
                            continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            //如果value没有实现IConvertible，此处默认不设置该属性，即该属性为默认值，值类型为0，引用为null
                            if (value is IConvertible)
                            {
                                pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType), null);
                            }
                            else
                            {
#if DEBUG
                                try
                                {
                                    pi.SetValue(t, value, null);
                                }
                                catch (InvalidCastException ie)
                                {
                                    throw new InvalidCastException("注意，非常重要！欲转换的DataTable列的类型必须实现Iconvertible或与对象类型一致。详细信息如下：" + ie.Message);
                                }
#else
                                try
                                {
                                    pi.SetValue( t, value, null );
                                }
                                catch( InvalidCastException )
                                {
                                }
#endif
                            }
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// 字符串转化为Int
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this string value, int defaultValue = 0)
        {
            var result = defaultValue;
            return int.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 字符串转化为decimal
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value, decimal defaultValue = 0)
        {
            var result = defaultValue;
            return decimal.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 两个对象是否相等
        /// </summary>
        /// <param name="value"></param>
        /// <param name="setValue"></param>
        /// <returns></returns>
        public static bool RequestToEquals(this object value, object setValue)
        {
            if (value == setValue) return true;
            if (value == null) return false;
            if (value.ToString() == setValue.ToString()) return true;
            if (value.ToInt() == setValue.ToInt()) return false;
            return false;
        }

        /// <summary>
        /// 截取字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="subNum">截取字符数</param>
        /// <returns></returns>
        public static string InterceptionStr(this string value, int subNum)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return string.Empty;

            if (value.Length > subNum)
                value = value.Substring(0, subNum) + "...";
            return value;
        }

        /// <summary>
        /// 对象转化为Int
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this object value, int defaultValue = 0)
        {
            if (value == null) return defaultValue;
            var result = defaultValue;
            return int.TryParse(value.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// 字符串转化为日期
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(this string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 根据字符串日期返回一个日期类型的字段
        /// </summary>
        /// <param name="strDate">字符串日期</param>
        /// <returns>日期类型的字段</returns>
        public static DateTime ToDateTime(this string strDate)
        {
            DateTime result;
            if (DateTime.TryParse(strDate, out result))
            {
                return result;
            }

            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        }

        /// <summary>
        /// 不规则格式如20150101 传入格式为"yyyyMMdd"
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object strDate, string format)
        {
            DateTime time;
            bool startresult = DateTime.TryParseExact(strDate.ToString(), format, null, DateTimeStyles.None, out time);
            if (!startresult)
            {
                time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); ;
            }
            return time;
        }

        /// <summary>
        /// 对象转化为日期
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(this object value)
        {
            DateTime result;
            if (DateTime.TryParse(value.ToString(), out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 获取图片绝对地址
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string SafeImgPath(this string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
                return "";

            if (filepath.StartsWith("http", false, CultureInfo.CurrentCulture))
                return filepath;
            return Config.PageSize + "/" + new ResourceCode().GetPathCode(filepath) + filepath;
        }

        /// <summary>
        /// 对象转化为时间字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="str">返回的字符串</param>
        /// <returns></returns>
        public static string ToStrDateTime(this object value, string str = "")
        {
            if (value == null) return str;
            DateTime dateTime;
            if (DateTime.TryParse(value.ToString(), out dateTime))
            {
                if (dateTime.Year == 1 || dateTime.Year == 9999)
                    return str;
                return dateTime.ToString();
            }
            return str;
        }

        /// <summary>
        /// 返回Request的字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string RequestToString(this object obj, string strValue = "")
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return strValue;
            return obj.ToString();
        }

        /// <summary>
        /// 返回Request的字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static int RequestToInt(this object obj, int strValue = 0)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return strValue;
            int.TryParse(obj.ToString(), out strValue);
            return strValue;
        }

        ///<summary>
        // 过滤SQL字符。
        /// </summary> 
        /// <param name="str"> 要过滤SQL字符的字符串。 </param> 
        /// <returns> 已过滤掉SQL字符的字符串。 </returns> 
        public static string ReplaceSQLChar(this string str)
        {
            if (str == String.Empty)
                return String.Empty;
            str = str.Replace("'", "‘");
            str = str.Replace(";", "；");
            str = str.Replace(",", ",");
            str = str.Replace("?", "?");
            str = str.Replace("<", "＜");
            str = str.Replace(">", "＞");
            str = str.Replace("(", "(");
            str = str.Replace(")", ")");
            str = str.Replace("@", "＠");
            str = str.Replace("=", "＝");
            str = str.Replace("+", "＋");
            str = str.Replace("*", "＊");
            str = str.Replace("&", "＆");
            str = str.Replace("#", "＃");
            str = str.Replace("%", "％");
            str = str.Replace("\"", "”");
            return str;
        }


        /// <summary>
        /// 返回枚举类型的中文描述 DescriptionAttribute 指定的名字
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="Enum"></param>
        /// <returns></returns>
        public static string Description<TEnum>(this TEnum Enum) where TEnum : struct
        {
            var em = Enum.ToString();
            var emArr = em.Split(',');
            return emArr.Aggregate("", (current, s) => current + ((TEnum)System.Enum.Parse(typeof(TEnum), s)).GetDescription());
        }

        private static string GetDescription<TEnum>(this TEnum Enum) where TEnum : struct
        {
            var em = Enum.ToString();
            FieldInfo fieldInfo = Enum.GetType().GetField(em);
            if (fieldInfo == null) return em;
            var attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length < 1) return em;
            return attributes[0].Description;
        }

        ///// <summary>
        ///// 根据DataTable输入Excel
        ///// </summary>
        ///// <param name="dt">DataTable</param>
        ///// <param name="fileName">文件名</param>
        public static void Export2Excel(this DataTable dt, string fileName = null)
        {
            HttpContext context = HttpContext.Current;
            HtmlTextWriter htmlWriter = null;

            if (dt == null) { return; }

            context.Response.Write(htmlWriter);
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Charset = "";

            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + (fileName ?? DateTime.Now.ToString("yyyyMMddHHmmss")) + ".xls");

            var strOur = new StringWriter();
            htmlWriter = new HtmlTextWriter(strOur);
            var dgrid = new DataGrid { DataSource = dt.DefaultView, AllowPaging = false };
            dgrid.DataBind();

            dgrid.RenderControl(htmlWriter);
            context.Response.Write("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><style>td{vnd.ms-excel.numberformat:@}</style></head>");
            context.Response.Write(strOur.ToString());
            context.Response.End();
        }


        /// <summary>
        /// 获取Hashtable键值，并返回指定类型的值（支持设置默认值）转换不成功，不抛出异常，返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hr"></param>
        /// <param name="keyStr"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T TryGetValue<T>(this Hashtable hr, string keyStr, T defaultValue = default(T))// where T : struct
        {
            var va = hr[keyStr];
            if (va != null)
            {
                try
                {
                    return (T)Convert.ChangeType(va, typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }
        /// <summary>
        /// 格式化DateTime?
        /// </summary>
        /// <param name="nullablDateTime">DateTime?</param>
        /// <param name="Format">Format</param>
        /// <returns></returns>
        public static string FormatNullableDateTime(this DateTime? nullablDateTime, string Format)
        {
            try
            {
                if (nullablDateTime.HasValue)
                {
                    return nullablDateTime.Value.ToString(Format);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static DateTime TimeStampToDataTime(this string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
    }
}
