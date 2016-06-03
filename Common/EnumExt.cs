using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Common
{
    /// <summary>
    /// 把枚举转换成SelectListItem
    /// </summary>
    public static class EnumExt
    {
        /// <summary>
        /// 返回枚举类型的中文描述 DescriptionAttribute 指定的名字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string DisplayName<T, TEnum>(this T t)
            where T : struct
            where TEnum : struct
        {
            var returnStr = "";
            var em = (TEnum) Enum.ToObject(typeof (TEnum), t);
            var emArr = em.ToString().Split(',');
            foreach (var s in emArr)
            {
                returnStr += ((TEnum)System.Enum.Parse(typeof(TEnum), s)).GetDescription();
            }
            return returnStr;
        }
        private static string GetDescription<TEnum>(this TEnum Enum) where TEnum : struct
        {
            var em = Enum.ToString();
            FieldInfo fieldInfo = Enum.GetType().GetField(em);
            if (fieldInfo == null) return em;
            var attributes =
                (EnumDisplayNameAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDisplayNameAttribute), false);
            if (attributes.Length < 1) return em;
            return attributes[0].DisplayName;
        }
        /// <summary>
        /// 获取枚举成员的自定义Attribute的一个属性值
        /// </summary>
        /// <param name="e">枚举成员</param>
        /// <returns></returns>
        public static string GetEnumDescription(object e)
        {
            //获取枚举成员的Type对象
            Type t = e.GetType();
            //获取Type对象的所有字段
            FieldInfo[] ms = t.GetFields();
            //遍历所有字段
            foreach (FieldInfo f in ms)
            {
                if (f.Name != e.ToString())
                {
                    continue;
                }
                if (f.IsDefined(typeof(EnumDisplayNameAttribute), true))
                {
                    return (f.GetCustomAttributes(typeof(EnumDisplayNameAttribute), true)[0] as EnumDisplayNameAttribute).DisplayName;
                }
            }
            return e.ToString();
        }
        public static List<SelectListItem> GetSelectList(Type enumType)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (object e in System.Enum.GetValues(enumType))
            {
                selectList.Add(new SelectListItem { Text = GetEnumDescription(e), Value = (Convert.ToInt32(e)).ToString() });
            }
            return selectList;
        }
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj, int selected)
        {
            try
            {
                var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                             select new SelectListItem { Value = (Convert.ToInt32(e).ToString()), Text = e.ToString() };
                return new SelectList(values, "Value", "Text", selected);
            }
            catch
            {
                return new SelectList(new List<string> { "转换枚举类型到列表时出现错误" }, selected);
            }
        }
    }
}
