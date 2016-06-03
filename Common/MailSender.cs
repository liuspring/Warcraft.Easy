using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;
using log4net;
using System.ComponentModel;
using System.Web;

namespace Common
{
    public class MailSender
    {
        static string _smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
        static string _userName = ConfigurationManager.AppSettings["UserName"];
        static string _pwd = ConfigurationManager.AppSettings["Pwd"];
        static string _authorName = ConfigurationManager.AppSettings["AuthorName"];
        static string _to = ConfigurationManager.AppSettings["To"];


        /// <summary>smtp服务器地址</summary>
        public string SmtpServer
        {
            get { return _smtpServer; }
            set { _smtpServer = value; }
        }

        /// <summary>发件人</summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>密码</summary>
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        /// <summary>显示名称</summary>
        public string AuthorName
        {
            get { return _authorName; }
            set { _authorName = value; }
        }

        /// <summary>收件人</summary>
        public string To
        {
            get { return _to; }
            set { _to = value; }
        }


        /// <summary>发送邮件</summary>
        /// <param name="subject">标题</param>
        /// <param name="body">邮件内容</param>
        public static void Send(string subject, string body)
        {
            try
            {
                var client = new SmtpClient(_smtpServer)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(_userName, _pwd),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                var mail = new MailMessage { From = new MailAddress(_userName, _authorName) };
                var toList = UtilityInfo.GetSubStringList(UtilityInfo.ToDbc(_to), ',');
                foreach (var s in toList)
                {
                    mail.To.Add(s);
                }
                mail.Subject = subject + "(" + Environment.MachineName + ")";
                mail.BodyEncoding = Encoding.Default;
                mail.Body = body;
                mail.IsBodyHtml = true;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                ILog log = LogManager.GetLogger(typeof(MailSender));
                log.Error(ex);
                log.Info(string.Format("mail发送错误，title={0},body={1}", subject, body));
            }
        }

        /// <summary>
        /// 发送邮件到指定人的邮箱
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="address">发送地址用","分割</param>
        public void Send(string subject, string body, string address)
        {
            try
            {
                var client = new SmtpClient(_smtpServer)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(_userName, _pwd),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                var mail = new MailMessage { From = new MailAddress(_userName, _authorName) };
                var toList = UtilityInfo.GetSubStringList(UtilityInfo.ToDbc(address), ',');
                foreach (var s in toList)
                {
                    mail.To.Add(s);
                }
                mail.Subject = subject + "(" + Environment.MachineName + ")";
                mail.BodyEncoding = Encoding.Default;
                mail.Body = body;
                mail.IsBodyHtml = true;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                ILog log = LogManager.GetLogger(typeof(MailSender));
                log.Error(ex);
                log.Info(string.Format("mail发送错误，title={0},body={1}", subject, body));
            }
        }

		void sc_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                ILog log = LogManager.GetLogger(typeof(MailSender));
                log.Info(string.Format("mail发送成功，email={0}",  e.UserState.ToString() ));
            }
            else
            {
                ILog log = LogManager.GetLogger(typeof(MailSender));
                log.Error(e.Error);
                log.Info(string.Format("mail发送错误，email={0}", e.UserState.ToString()));
            }
        }
        public static bool SendMail(string sendTo,string title,string content)
        {
            const string pv = "1.0.0";
            var url = System.Configuration.ConfigurationManager.AppSettings["SendEmailUrl"];
            var sign = Common.UtilityInfo.Md5(content + pv + "111");
            //先生成 校验码 原始值 MD5
            //content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content));
            //content = HttpUtility.HtmlEncode(HttpUtility.UrlEncode(content));
            //title = HttpUtility.HtmlEncode(HttpUtility.UrlEncode(title));
            //先对内容进行UrlEncode，然后再HtmlEncode，将处理后的结果发送到接口

            var data =
                string.Format(
                    "sendTo={0}&content={1}&title={2}&warnType={3}&emaliType={4}&jsonType={5}&sign={6}&pv={7}", sendTo,
                    content, title, 1, 1, 1, sign, pv);
            string ret;
            HttpWebAsk.Post(url, data, out ret);
            var td = Common.JsonHelper.DeSerialize<trade>(ret);
            return td.id == 1;
        }


        public static bool SendMailNew(string sendTo, string title, string content,int eType=0)
        {
            const string pv = "1.0.0";
            var account = System.Configuration.ConfigurationManager.AppSettings["MailAccount"];
            var password = System.Configuration.ConfigurationManager.AppSettings["MailPassword"];
            var proId = System.Configuration.ConfigurationManager.AppSettings["MailProld"];
            var url = System.Configuration.ConfigurationManager.AppSettings["SendEmailUrl"];
            content = "12313213213aadsfasdfasdfasdf";// HttpUtility.UrlEncode(content,System.Text.Encoding.UTF8);
            var sign =
                Common.UtilityInfo.Md5(account +"yTg/+Jp8Vk" +password);

            var data =
                string.Format(
                    "account={0}&password={1}&proId={2}&sendTo={3}&content={4}&title={5}&eType={6}&isDes={7}&jsonType={8}&sign={9}", account,
                    password, proId, sendTo, content,title, eType,"false",1,sign);
            string ret;
            HttpWebAsk.Post(url, data, out ret);
            var td = Common.JsonHelper.DeSerialize<trade>(ret);
            return td.id == 1;
        }


        /// <summary>
        /// 发送邮件到指定人的邮箱
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="address">发送地址用","分割</param>
        public void SendAsync(string subject, string body, string address)
        {
            try
            {
                var client = new SmtpClient(_smtpServer)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(_userName, _pwd),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                client.SendCompleted += new SendCompletedEventHandler(sc_SendCompleted);

                var mail = new MailMessage { From = new MailAddress(_userName, _authorName) };
                var toList = UtilityInfo.GetSubStringList(UtilityInfo.ToDbc(address), ',');
                foreach (var s in toList)
                {
                    mail.To.Add(s);
                }
                mail.Subject = subject + "(" + Environment.MachineName + ")";
                mail.BodyEncoding = Encoding.Default;
                mail.Body = body;
                mail.IsBodyHtml = true;
                client.SendAsync(mail, address);
            }
            catch (Exception ex)
            {
                ILog log = LogManager.GetLogger(typeof(MailSender));
                log.Error(ex);
                log.Info(string.Format("mail发送错误，title={0},body={1}", subject, body));
            }
        }


        public static bool Send(string subject, string body, string address, int warnType, int emaliType)
        {
            
           // HttpHelper.Post()

                return false;
        }
    }
    public class trade
    {
        public string responseType { get; set; }
        public string tradeTime { get; set; }
        public int id { get; set; }
        public string comment { get; set; }
    }
}
