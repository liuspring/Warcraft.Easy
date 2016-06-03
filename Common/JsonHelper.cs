﻿using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
namespace Common
{
    public class JsonHelper
    {
        public static string GetMessage(bool result, string msg)
        {
            var t = new Dictionary<string, string> { { "result", result ? "true" : "false" }, { "msg", msg } };
            return Serialize(t);
        }
        public static string GetMessage(bool result, object msg)
        {
            var t = new Dictionary<string, object> { { "result", result ? "true" : "false" }, { "msg", msg } };
            return Serialize(t);
        }
        public static string String2JsonStr(string str)
        {
            str = string.IsNullOrEmpty(str) ? "" : str.Trim();
            str = str.Replace("\"", "&quot;");
            str = str.Replace("'", "&#39;");
            str = str.Replace("\r", "\\r");
            str = str.Replace("\n", "\\n");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace(" ", "&nbsp;");
            return str;
        }

        /// <summary>
        ///   序列化简单对象
        /// </summary>
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, new IsoDateTimeConverter());
            //return JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }

        /// <summary>
        ///   反序列话简单对象
        /// </summary>
        public static T DeSerialize<T>(string value)
        {
            if (value == "{}")
                return default(T);
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 对象转对象   
        /// </summary>
        public static T DeSerialize<T>(object obj)
        {
            return JsonConvert.DeserializeObject<T>(Serialize(obj));
        }

        /// <summary>
        ///   反序列话简单对象
        /// </summary>
        public static T DeSerialize<T>(string value, out string parseError)
        {
            string errormsg = string.Empty;
            T objT = default(T);
            try
            {
                objT = JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings()
                {
                    Error = delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
                    {
                        errormsg = string.Format("字段:{0},异常:{1}", e.ErrorContext.Member, e.ErrorContext.Error.Message);
                        //e.ErrorContext.Handled = true;
                    }
                });
                return objT;
            }
            finally
            {
                parseError = errormsg;
            }
        }

        /// <summary>
        /// 获取json格式table
        /// </summary>
        /// <param name="total">总记录数</param>
        /// <param name="pageCount">页数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="list">泛型集合</param>
        /// <param name="type">1为泛型集合,2为DataTable或DataSet</param>
        /// <returns>json字符串</returns>
        public static string GetListJsonStr(int total, int pageCount, int pageIndex, int pageSize, object list, int type)
        {
            try
            {
                string tpl = "{PageCount:\"" + pageCount + "\",PageSize:\"" + pageSize + "\",Total:\"" + total + "\",PageIndex:\"" + pageIndex + "\"},";
                string jsonList = JsonConvert.SerializeObject(list, new IsoDateTimeConverter());
                return jsonList.Insert(1, tpl);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static List<string> GetListByJsonStr(string msgStr)
        {
            List<string> itemes = new List<string>();
            if (!string.IsNullOrEmpty(msgStr))
            {
                try
                {
                    using (JsonReader jsonReader = new JsonTextReader(new StringReader(msgStr)))
                    {
                        while (jsonReader.Read())
                        {
                            if (jsonReader.Value != null && !string.IsNullOrEmpty(jsonReader.Value.ToString()))
                                itemes.Add(jsonReader.Value.ToString());
                        }
                    }
                    return itemes;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
                return null;
        }
        public static object Deserialize(string jsonstr, Type jsonDataType)
        {
            return JsonConvert.DeserializeObject(jsonstr, jsonDataType);
        }
        /// <summary>
        /// 获取json格式table
        /// </summary>
        /// <param name="total">总记录数</param>
        /// <param name="pageCount">页数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="list">泛型集合</param>
        /// <param name="type">1为泛型集合,2为DataTable或DataSet</param>
        /// <returns>json字符串</returns>
        public static string GetListJsonString(int total, int pageCount, int pageIndex, int pageSize, object list)
        {
            try
            {
                var jsonList = JsonConvert.SerializeObject(list, new IsoDateTimeConverter());
                return "{pageCount:" + pageCount + ",pageSize:" + pageSize + ",total:" + total + ",pageIndex:" + pageIndex + ",list: " + jsonList + " }";
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
