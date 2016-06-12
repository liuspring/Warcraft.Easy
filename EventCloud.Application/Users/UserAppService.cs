using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using EventCloud.Users.Dto;

namespace EventCloud.Users
{
    /* THIS IS JUST A SAMPLE. */
    public class UserAppService : EventCloudAppServiceBase, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly IPermissionManager _permissionManager;

        public UserAppService(UserManager userManager, IPermissionManager permissionManager)
        {
            _userManager = userManager;
            _permissionManager = permissionManager;
        }

        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await _userManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await _userManager.RemoveFromRoleAsync(userId, roleName));
        }

        public List<AccountListOutput> FindAccountList(AccountListInput input)
        {
            List<AccountListOutput> accountList;
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                //users = _userManager.Users.OrderBy(a => a.Id).Skip(input.iDisplayStart).Take(input.iDisplayLength).ToList<User>();
               var users = _userManager.Users.ToList();
                accountList = users.MapTo<List<AccountListOutput>>();
            }
            return accountList;
        }

        public int FindAccountListTotal(AccountListInput input)
        {
            int count;
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                count = _userManager.Users.Count();
            }
            return count;
        }
    }
}