using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Abp.Application.Navigation;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using EventCloud.Sessions.Dto;

namespace EventCloud.Sessions
{
    [AbpAuthorize]
    public class SessionAppService : EventCloudAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                User = (await GetCurrentUserAsync()).MapTo<UserLoginInfoDto>(),
               
            };
            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = (await GetCurrentTenantAsync()).MapTo<TenantLoginInfoDto>();
            }

            return output;
        }
    }
}