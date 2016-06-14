using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using AutoMapper;
using EventCloud.MultiTenancy;
using EventCloud.Users.Dto;
using Abp.Extensions;
using Microsoft.AspNet.Identity;

namespace EventCloud.Users
{
    /* THIS IS JUST A SAMPLE. */
    public class UserAppService : EventCloudAppServiceBase, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly TenantManager _tenantManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IPermissionManager _permissionManager;
        private readonly IRepository<User, long> _useRepository;

        public UserAppService(TenantManager tenantManager,
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager,
            IPermissionManager permissionManager,
            IRepository<User, long> useRepository)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _permissionManager = permissionManager;
            _useRepository = useRepository;
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

        public async Task<ListResultOutput<UserListOutput>> GetList(UserListInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var users =
                    await
                        _userManager.Users.OrderBy(a => a.Id)
                            .Skip(input.iDisplayStart)
                            .Take(input.iDisplayLength)
                            .ToListAsync();
                return new ListResultOutput<UserListOutput>(users.MapTo<List<UserListOutput>>());
            }
        }

        public async Task<int> GetListTotal(UserListInput input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var users = await _userManager.Users.ToListAsync();
                return users.Count;
            }
        }

        public async Task Save(CreateUserInput input)
        {
            var tenant = await GetActiveTenantAsync(Tenant.DefaultTenantName);
            var user = new User
            {
                Id = input.Id,
                TenantId = tenant.Id,
                Name = input.Name,
                Surname = input.Surname,
                EmailAddress = input.EmailAddress,
                IsActive = true
            };
            //Username and Password are required if not external login
            if (input.UserName.IsNullOrEmpty() || input.Password.IsNullOrEmpty())
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
            user.UserName = input.UserName;
            user.Password = new PasswordHasher().HashPassword(input.Password);

            //Switch to the tenant
            //_unitOfWorkManager.Current.EnableFilter(AbpDataFilters.MayHaveTenant);
            //_unitOfWorkManager.Current.SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, tenant.Id);
            //Add default roles

            //Save user
            if (user.Id == 0)
            {
                await _useRepository.InsertAsync(user);
                //CheckErrors(await _userManager.CreateAsync(user));
            }
            else
            {
                await _useRepository.UpdateAsync(user);
                //CheckErrors(await _userManager.UpdateAsync(user));
            }
        }

        #region Common private methods

        private async Task<Tenant> GetActiveTenantAsync(string tenancyName)
        {
            var tenant = await _tenantManager.FindByTenancyNameAsync(tenancyName);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIsNotActive", tenancyName));
            }

            return tenant;
        }

        #endregion
    }
}