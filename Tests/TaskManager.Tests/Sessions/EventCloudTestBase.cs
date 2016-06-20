using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Collections;
using Abp.Configuration.Startup;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Runtime.Session;
using Abp.TestBase;
using TaskManager.EntityFramework;
using TaskManager.Migrations.SeedData;
using TaskManager.MultiTenancy;
using TaskManager.Users;
using Castle.MicroKernel.Registration;
using EntityFramework.DynamicFilters;
using TaskManager.Domain.Events;
using TaskManager.Tests.Data;

namespace TaskManager.Tests.Sessions
{
    public abstract class TaskManagerTestBase : AbpIntegratedTestBase
    {
        static TaskManagerTestBase()
        {
            //Disable global event bus for unit tests
            DomainEvents.EventBus = NullEventBus.Instance;
        }

        protected TaskManagerTestBase()
        {
            //Seed initial data
            UsingDbContext(context => new InitialDataBuilder(context).Build());
            UsingDbContext(context => new TestDataBuilder(context).Build());

            LoginAsDefaultTenantAdmin();
        }

        protected override void PreInitialize()
        {
            base.PreInitialize();

            //Fake DbConnection using Effort!
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>()
                    .UsingFactoryMethod(Effort.DbConnectionFactory.CreateTransient)
                    .LifestyleSingleton()
                );
        }

        protected override void AddModules(ITypeList<AbpModule> modules)
        {
            base.AddModules(modules);

            //Adding testing modules. Depended modules of these modules are automatically added.
            modules.Add<TaskManagerApplicationModule>();
            modules.Add<TaskManagerDataModule>();
        }

        public void UsingDbContext(Action<TaskManagerDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<TaskManagerDbContext>())
            {
                context.DisableAllFilters();
                action(context);
                context.SaveChanges();
            }
        }
        
        public async Task UsingDbContext(Func<TaskManagerDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<TaskManagerDbContext>())
            {
                context.DisableAllFilters();
                await action(context);
                await context.SaveChangesAsync();
            }
        }

        public T UsingDbContext<T>(Func<TaskManagerDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<TaskManagerDbContext>())
            {
                context.DisableAllFilters();
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected void LoginAsHostAdmin()
        {
            LoginAsHost(User.AdminUserName);
        }

        protected void LoginAsDefaultTenantAdmin()
        {
            LoginAsTenant(Tenant.DefaultTenantName, User.AdminUserName);
        }

        protected void LoginAsHost(string userName)
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = true;

            AbpSession.TenantId = null;

            var user = UsingDbContext(context => context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName));
            if (user == null)
            {
                throw new Exception("There is no user: " + userName + " for host.");
            }

            AbpSession.UserId = user.Id;
        }

        protected void LoginAsTenant(string tenancyName, string userName)
        {
            var tenant = UsingDbContext(context => context.Tenants.FirstOrDefault(t => t.TenancyName == tenancyName));
            if (tenant == null)
            {
                throw new Exception("There is no tenant: " + tenancyName);
            }

            AbpSession.TenantId = tenant.Id;

            var user = UsingDbContext(context => context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName));
            if (user == null)
            {
                throw new Exception("There is no user: " + userName + " for tenant: " + tenancyName);
            }

            AbpSession.UserId = user.Id;
        }

        /// <summary>
        /// Gets current user if <see cref="IAbpSession.UserId"/> is not null.
        /// Throws exception if it's null.
        /// </summary>
        protected async Task<User> GetCurrentUserAsync()
        {
            var userId = AbpSession.GetUserId();
            return await UsingDbContext(context => context.Users.SingleAsync(u => u.Id == userId));
        }

        /// <summary>
        /// Gets current tenant if <see cref="IAbpSession.TenantId"/> is not null.
        /// Throws exception if there is no current tenant.
        /// </summary>
        protected async Task<Tenant> GetCurrentTenantAsync()
        {
            var tenantId = AbpSession.GetTenantId();
            return await UsingDbContext(context => context.Tenants.SingleAsync(t => t.Id == tenantId));
        }
    }
}