using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Common
{
    [Flags]
    public enum CustomerServiceError
    {
        [Description("操作成功！")]
        Success = 1,
    };

    public class Error
    {
        private Dictionary<Int64, String> err_desc_;

        public static readonly Int32 SUCC;
        public static readonly Int32 UNKNOWN_ERROR = ERROR_MAX;

        #region error code
        public static readonly Int32 ERROR_MAX = -100000;
        public static readonly Int32 COMM_MIN = -999999;

        public static readonly Int32 DB_ERR_UNKNOWN = ERROR_MAX - 51;
        public static readonly Int32 NOT_DB_EXCEPTION = ERROR_MAX - 52;
        #endregion

        public static Boolean SUCCEEDED(Int32 err_code) { return err_code >= SUCC; }
        public static Boolean FAILED(Int32 err_code) { return !SUCCEEDED(err_code); }

        public Error()
        {
            err_desc_ = new Dictionary<Int64, String>();
            RegisterErrCode(SUCC, "操作成功");
        }
        public String GetError(Int32 err_code)
        {
            if (err_desc_.ContainsKey(err_code))
                return err_desc_[err_code];
            else
                return "未知错误";
        }
        protected void RegisterErrCode(Int64 err_code, String desc) { err_desc_.Add(err_code, desc); }
    }
}
