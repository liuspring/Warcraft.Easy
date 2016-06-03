using System;
using System.IO;
using System.Net;
using System.Text;

namespace Common
{
    /// <summary>
    /// Http 请求
    /// </summary>
    /// <remarks>创建人员(日期):(111026 17:36)</remarks>
    public class HttpHelper
    {
        /// <summary>
        /// 使用 UTF8 编码后，向指定 URL Post 数据（请求超时 5 秒）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string Post(string url, string postData)
        {
            return Post(url, postData, 5000, new UTF8Encoding());
        }
        /// <summary>
        /// 使用 UTF8 编码后，向指定 URL Post 数据（请求超时 5 秒）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string PostJson(string url, string postData)
        {
            return Post(url, postData, 5000, new UTF8Encoding()
                , "application/json;charset=UTF-8");
        }
        /// <summary>
        /// 使用 UTF8 编码后，向指定 URL Post 数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="timeout">请求超时时间</param>
        /// <returns></returns>
        public static string Post(string url, string postData, int timeout)
        {
            return Post(url, postData, timeout, new UTF8Encoding());
        }
        /// <summary>
        /// 向指定 URL Post 数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="timeout">请求超时时间</param>
        /// <param name="encoding">规则编码</param>
        /// <param name="contentType">获取或设置 Content-typeHTTP 标头的值</param>
        /// <returns></returns>
        public static string Post(string url, string postData, int timeout, Encoding encoding, string contentType = "")
        {
            string content = string.Empty;
            //ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data;
            if (postData == null)
            {
                data = new byte[]{};
            }
            else
            {
                data = encoding.GetBytes(postData);
            }
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.Timeout = timeout;
            //如果post 是 json 需要修改 contentType
            if (string.IsNullOrWhiteSpace(contentType))
                myRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            else
                myRequest.ContentType = contentType;

            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }
            return content;
        }

        /// <summary>
        /// 向指定 URL Post 数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static string Get(string url, string queryStr)
        {
            string _url = url;
            if (!string.IsNullOrEmpty(queryStr))
                _url = string.Format("{0}?{1}", url, queryStr);

            ASCIIEncoding encoding = new ASCIIEncoding();
            //byte[] data = encoding.GetBytes(postData);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(_url);
            myRequest.Method = "GET";
            //myRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //myRequest.ContentLength = data.Length;
            //Stream newStream = myRequest.GetRequestStream();
            //newStream.Write(data, 0, data.Length);
            //newStream.Close();
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();

            return content;
        }

        /// <summary>
        /// 请求webApi
        /// </summary>
        /// <param name="weburl"></param>
        /// <param name="data"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string PushToWebApi(string weburl, string data, Encoding encode)
        {
            byte[] byteArray = encode.GetBytes(data);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(weburl));
            webRequest.Method = "POST";
            webRequest.Accept = "application/xml";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = byteArray.Length;
            using (Stream newStream = webRequest.GetRequestStream())
            {
                newStream.Write(byteArray, 0, byteArray.Length);
            }

            //接收返回信息：
            var str = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
            {
                using (StreamReader aspx = new StreamReader(response.GetResponseStream(), encode))
                {
                    str = aspx.ReadToEnd();
                }
            }
            return str;
        }


    }
}

