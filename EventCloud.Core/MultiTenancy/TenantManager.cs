using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using TaskManager.Authorization.Roles;
using TaskManager.Editions;
using TaskManager.Users;

namespace TaskManager.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager
            ) : base(
            tenantRepository, 
            tenantFeatureRepository, 
            editionManager)
        {
        }
    }
}