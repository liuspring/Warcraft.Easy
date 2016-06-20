using Abp.Authorization;
using TaskManager.Authorization.Roles;
using TaskManager.MultiTenancy;
using TaskManager.Users;

namespace TaskManager.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
