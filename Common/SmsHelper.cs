using System;

namespace Common
{
    public static class SmsHelper
    {
        public static bool SendSms(string phone, string content)
        {
           
                SmsSettingModel info = JsonHelper.DeSerialize<SmsSettingModel>(Config.PageSize);
                if (info == null)
                    throw new Exception("缺少短信配置节点 Sms-Settings");

                info.phone = phone;
                info.smsContent = string.Format("{1}{0}", content, info.sign);
                var pars = string.Format("account={0}&password={1}&mobile={2}&smsContent={3}&jsonType={4}",
                    info.account,
                    info.password,
                    info.phone,
                    info.smsContent,
                    info.jsonType);
                var json = HttpHelper.Post(info.Api, pars);
                var result = JsonHelper.Serialize(json);
                return true;
        }
    }
    public class SmsSettingModel
    {
        public string Api { set; get; }
        public string account { set; get; }
        public string password { set; get; }
        public string sign { set; get; }
        public string phone { set; get; }
        public string smsContent { set; get; }
        /// <summary>
        /// 为 1 时，接口返回 json
        /// </summary>
        public string jsonType { set; get; }
    }

    public class SmsResult
    {
        public string id { set; get; }
        public string responseType { set; get; }
        public string tradeTime { set; get; }
        public string comment { set; get; }
    }
}
