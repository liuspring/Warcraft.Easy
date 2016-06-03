using System;
using System.IO;
using System.Web;
using Commons.Collections;
using NVelocity;
using NVelocity.App;
using NVelocity.Context;
using NVelocity.Runtime;

namespace Common {
    public class VelocityHelper {
        private VelocityEngine _mVelocity;
        private IContext _mContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templatDir">模板文件夹路径</param>
        public VelocityHelper(string templatDir) {
            Init(templatDir);
        }
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public VelocityHelper() { }
        /// <summary>
        /// 初始话NVelocity模块
        /// </summary>
        /// <param name="templatDir">模板文件夹路径</param>
        public void Init(string templatDir) {
            //创建VelocityEngine实例对象
            _mVelocity = new VelocityEngine();

            //使用设置初始化VelocityEngine
            var props = new ExtendedProperties();
            props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH,
                              templatDir.IndexOf(':', 0, 2) > 0
                                  ? templatDir
                                  : HttpContext.Current.Server.MapPath(templatDir));
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
            props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");
            _mVelocity.Init(props);
            //为模板变量赋值
            _mContext = new VelocityContext();
        }
        /// <summary>
        /// 给模板变量赋值
        /// </summary>
        /// <param name="key">模板变量</param>
        /// <param name="value">模板变量值</param>
        public void Put(string key, object value) {
            if (_mContext == null)
                _mContext = new VelocityContext();
            _mContext.Put(key, value);
        }
        /// <summary>
        /// 显示模板
        /// </summary>
        /// <param name="templatFileName">模板文件名</param>
        public void Display(string templatFileName) {
            //从文件中读取模板
            Template template = _mVelocity.GetTemplate(templatFileName);
            //合并模板
            var writer = new StringWriter();
            template.Merge(_mContext, writer);
            //输出
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.Write(writer.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 获取模板页面信息
        /// </summary>
        /// <param name="templatFileName">模板文件名</param>
        public string GetHtml(string templatFileName) {
            //从文件中读取模板
            Template template = _mVelocity.GetTemplate(templatFileName);
            //合并模板
            var writer = new StringWriter();
            template.Merge(_mContext, writer);
            //输出
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ContentType = "text/html";
            return writer.ToString();
        }
    }
}
