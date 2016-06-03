using System;
using System.Configuration;

namespace Common
{
    public class Config
    {
        /// <summary>
        /// 请求超时设置（以毫秒为单位），默认为10秒。
        /// 说明：此处常量专为提供给方法的参数的默认值，不是方法内所有请求的默认超时时间。
        /// </summary>
        public const int TIME_OUT = 10000;

        private static readonly object MLocker = new Object();
        private static string _mPageSize;
        public static int PageSize
        {
            get
            {
                if (string.IsNullOrEmpty(_mPageSize))
                {
                    lock (MLocker)
                    {
                        if (string.IsNullOrEmpty(_mPageSize))
                        {
                            _mPageSize = ConfigurationManager.AppSettings["PageSize"];
                        }
                    }
                }
                return int.Parse(_mPageSize);
            }
        }

        /// <summary>
        /// 是否需要进行验证码：0不需要，1需要
        /// </summary>
        public static bool IsNeedVerifyCode
        {
            get { return ConfigurationManager.AppSettings["IsNeedVerifyCode"] == "1"; }
        }

    }
}
