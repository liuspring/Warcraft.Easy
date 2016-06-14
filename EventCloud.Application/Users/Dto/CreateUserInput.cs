using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EventCloud.MultiTenancy;

namespace EventCloud.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class CreateUserInput : IInputDto
    {

        public long Id { get; set; }
        /// <summary>
        /// Not required for single-tenant applications.
        /// </summary>
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        [StringLength(User.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(User.MaxPlainPasswordLength)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public CreateUserInput()
        {
            TenancyName = Tenant.DefaultTenantName;
        }
    }
}
