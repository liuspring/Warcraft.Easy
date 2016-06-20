using Abp.Application.Features;
using TaskManager.Authorization.Roles;
using TaskManager.MultiTenancy;
using TaskManager.Users;

namespace TaskManager.Features
{
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, Role, User>
    {
        public FeatureValueStore(TenantManager tenantManager)
            : base(tenantManager)
        {
        }
    }
}
