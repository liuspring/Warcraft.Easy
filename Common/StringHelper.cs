using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Common
{
    public static class StringHelper
    {
        public static int ConvertObjectToInt(object obj)
        {
           
                if (obj == null)
                {
                    return 0;
                }
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    return 0;
                }
                return int.Parse(obj.ToString());
            }

        public static int ConvertStringToInt(string obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(obj);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static long ConvertObjectToLong(object obj)
        {

            if (obj == null)
            {
                return 0;
            }
            if (string.IsNullOrEmpty(obj.ToString()))
            {
                return 0;
            }
            return long.Parse(obj.ToString());
        }

        public static string ConverObjectToString(object obj)
        {
            return obj == null ? "" : obj.ToString();
        }

        public static string DateTimeFormat(DateTime data)
        {

            return data.ToString().Replace("/", "-");
        }

        /// <summary>
        /// 将字符串格式化为卡号格式
        /// </summary>
        /// <param name="cardStr">字符串实例</param>
        /// <param name="splitCount">间隔字符数</param>
        /// <param name="groupIndex">替换为目标字符串(*号)的分组位置</param>
        /// <param name="replaceChar">替换的目标字符</param>
        /// <returns></returns>
        public static string ToCardFormatString(this string cardStr, int splitCount = 4, int groupIndex = 2, char replaceChar = '*')
        {
            if (string.IsNullOrWhiteSpace(cardStr) || cardStr.Length <= splitCount)
                return cardStr;
            cardStr = cardStr.Trim(' ', '　');
            var c = cardStr.ToCharArray();

            var n = cardStr.Length / splitCount;
            var j = cardStr.Length % splitCount;
            var groupCount = n + (j > 0 ? 1 : 0);
            var newLength = cardStr.Length + (groupCount - 1);
            var charArray = new char[newLength];
            var currentIndex = 0;
            for (int i = 0; i < groupCount; i++)
            {
                if (i + 1 == groupIndex)
                {
                    replaceChar.ToString(CultureInfo.InvariantCulture)
                               .PadLeft(splitCount, replaceChar)
                               .CopyTo(0, charArray, currentIndex,
                                       i == groupCount - 1 ? newLength - currentIndex : splitCount);
                }
                else
                {
                    cardStr.CopyTo(i * splitCount, charArray, currentIndex,
                                   i == groupCount - 1 ? newLength - currentIndex : splitCount);
                }

                currentIndex += splitCount;

                if (i < groupCount - 1) charArray[currentIndex++] = ' ';
            }
            return string.Join(string.Empty, charArray);
        }

    }
}
