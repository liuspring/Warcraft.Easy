using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TaskManager.Users.Dto;

namespace TaskManager.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<ListResultOutput<UserListOutput>> GetList(UserListInput input);

        Task<int> GetListTotal(UserListInput input);

        Task Save(CreateUserInput input);

    }
}