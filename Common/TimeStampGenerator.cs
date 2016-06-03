using System;
using System.Globalization;

namespace Common
{
    public static class TimeStampGenerator
    {
        /// <summary>
        /// 日期类型转换    
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentTimeStamp(DateTime dateTime)
        {
            TimeSpan span = dateTime.ToLocalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            return Convert.ToInt64(span.TotalSeconds);
        }
        public static DateTime UnixTimestampToDateTime(this DateTime target, long timestamp)
        {
            var start = new DateTime(1970, 1, 1, 8, 0, 0, target.Kind);
            return start.AddSeconds(timestamp);
        }
        public static DateTime UnixMiDateTime(long timestamp)
        {
            var start = new DateTime(1970, 1, 1, 8, 0, 0);
            return start.AddMilliseconds(timestamp);
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        public static long GetCurrentMillSeconds(DateTime time)
        {
            TimeSpan span = time.ToLocalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            return Convert.ToInt64(span.TotalMilliseconds);
        }
        public static long GetTimeSpanSeconds(TimeSpan ts)
        {
            return ((ts.Days * 24 + ts.Hours) * 60 + ts.Minutes) * 60 + ts.Seconds;
        }


        public static DateTime GetDateTimeByUnixTime(this long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp.ToString().PadRight(17, '0'));
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// 转换成13位时间戳
        /// </summary>
        /// <param name="datetime">DateTime</param>
        /// <returns></returns>
        public static long ToUnixTimestamp(this DateTime datetime)
        {
            return (datetime.ToUniversalTime().Ticks - 621355968000000000)/10000;
        }
    }
}
