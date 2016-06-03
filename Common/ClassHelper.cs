using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class ClassHelper
    {
        public static Dictionary<string, string> GetProperties<T>(this T t)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (t == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return null;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;                                                  //实体类字段名称
                string value = item.GetValue(t, null)==null?"":item.GetValue(t,null).ToString();                //该字段的值
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ret.Add(name, value);        //在此可转换value的类型
                }
            }
            return ret;
        }
    }
}
