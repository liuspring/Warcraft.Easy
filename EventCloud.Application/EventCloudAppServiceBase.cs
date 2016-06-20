using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using TaskManager.MultiTenancy;
using TaskManager.Users;
using Microsoft.AspNet.Identity;

namespace TaskManager
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class TaskManagerAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        public IUserNavigationManager UserNavigationManager { get; set; }

        protected TaskManagerAppServiceBase()
        {
            LocalizationSourceName = TaskManagerConsts.LocalizationSourceName;
        }
        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected virtual Task<IReadOnlyList<UserMenu>> GetMenus()
        {
            return UserNavigationManager.GetMenusAsync(AbpSession.UserId, AbpSession.TenantId);
        }
    }
}