using System.Collections.Generic;
using TaskManager.Commands.Dto;

namespace TaskManager.Commands
{
    public interface ICommandAppService
    {
        int Create(CreateCommandInput input);

        List<CommandListOutput> GetList(CommandListInput input);

        int GetListTotal(CommandListInput input);
    }
}
