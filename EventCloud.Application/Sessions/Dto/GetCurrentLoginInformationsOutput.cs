using System.Collections.Generic;
using Abp.Application.Navigation;
using Abp.Application.Services.Dto;

namespace TaskManager.Sessions.Dto
{
    public class GetCurrentLoginInformationsOutput : IOutputDto
    {
        public UserLoginInfoDto User { get; set; }

        public TenantLoginInfoDto Tenant { get; set; }

        public List<UserMenu> Menus { get; set; }
    }
}