using System.Collections.Generic;
using EventCloud.Commands.Dto;

namespace EventCloud.Commands
{
    public interface ICommandAppService
    {
        int Create(CreateCommandInput input);

        List<CommandListOutput> GetList(CommandListInput input);

        int GetListTotal(CommandListInput input);
    }
}
