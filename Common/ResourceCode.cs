using System;

namespace Common
{
    /// <summary>
    /// 资源加解密
    /// </summary>
    public class ResourceCode
    {
        /// <summary>
        /// 前后5分钟有效，当前时间取5的整数
        /// </summary>
        private int _minuteStep = 10;

        private int modNum;

        private readonly DateTime _dtNow = DateTime.Now;
        public ResourceCode()
        {
            //当前的分钟数
            int currentMinute = _dtNow.Minute;
            modNum = currentMinute % _minuteStep;
        }

        public string PathAddCode(string path)
        {
            return string.Format("/{0}{1}", GetPathCode(path), path);
        }
        public string GetPathCode(string path)
        {
            return GetPathCode(path, getSalt(0));
        }

        public bool CheckedPathCode(string path, string code)
        {
            string pathCode = GetPathCode(path, getSalt(0));
            if (code == pathCode)
            {
                return true;
            }
            pathCode = GetPathCode(path, getSalt(1));
            if (code == pathCode)
            {
                return true;
            }
            pathCode = GetPathCode(path, getSalt(-1));
            if (code == pathCode)
            {
                return true;
            }
            return false;
        }

        private string getSalt(int position)
        {
            DateTime dt = _dtNow.AddMinutes(0 - modNum).AddMinutes(_minuteStep * position);
            return dt.ToString("yyyy-MM-dd HH:mm");
        }
        private string GetPathCode(string path, string salt)
        {
            string str = path + "?" + salt;
            return GetMd5String(str);
        }

        #region 加 解密
        private string GetMd5String(string str)
        {

            string pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");

            return pwd;

        }

        #endregion
    }
}
