using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public class DateTimeHelper
    {
        /// <summary>
        /// 获取当前年份
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentYear()
        {
            return DateTime.Now.Year.ToString();
        }
    }
}
