using Abp.Authorization.Roles;
using TaskManager.MultiTenancy;
using TaskManager.Users;

namespace TaskManager.Authorization.Roles
{
    public class Role : AbpRole<Tenant, User>
    {

    }
}