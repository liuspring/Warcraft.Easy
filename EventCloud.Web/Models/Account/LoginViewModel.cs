using System.ComponentModel.DataAnnotations;
using EventCloud.MultiTenancy;

namespace EventCloud.Web.Models.Account
{
    public class LoginViewModel
    {
        public string TenancyName
        {
            get
            {
                if (UsernameOrEmailAddress == "admin")
                    return Tenant.DefaultTenantName;
                return string.Empty;
            }
        }

        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public string VerifyCode { get; set; }

        public bool RememberMe { get; set; }
    }
}