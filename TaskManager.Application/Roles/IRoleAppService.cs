using System.Threading.Tasks;
using Abp.Application.Services;
using TaskManager.Roles.Dto;

namespace TaskManager.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
