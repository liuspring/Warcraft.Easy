using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Abp.Application.Navigation;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using TaskManager.Sessions.Dto;

namespace TaskManager.Sessions
{
    [AbpAuthorize]
    public class SessionAppService : TaskManagerAppServiceBase, ISessionAppService
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