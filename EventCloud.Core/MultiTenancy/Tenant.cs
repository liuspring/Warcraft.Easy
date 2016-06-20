using Abp.MultiTenancy;
using TaskManager.Users;

namespace TaskManager.MultiTenancy
{
    public class Tenant : AbpTenant<Tenant, User>
    {

    }
}