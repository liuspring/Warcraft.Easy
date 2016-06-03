using System.Web;
using System.Web.SessionState;

namespace Common
{
    /// <summary>
    /// session帮助类
    /// </summary>
    public abstract class SessionHelper : IRequiresSessionState
    {
        /// <summary> 设置Session </summary>
        public static void Set(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
        /// <summary> 获取Session </summary>
        public static object Get(string key)
        {
            return HttpContext.Current.Session[key];
        }
    }
}
