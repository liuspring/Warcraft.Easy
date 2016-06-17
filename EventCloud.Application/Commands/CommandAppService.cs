using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using EventCloud.Commands.Dto;
using EventCloud.Tasks;

namespace EventCloud.Commands
{
    public class CommandAppService : EventCloudAppServiceBase, ICommandAppService
    {

        private readonly IRepository<Command, int> _commandRepository;

        public CommandAppService(IRepository<Command, int> commandRepository)
        {
            _commandRepository = commandRepository;
        }
        public int Create(CreateCommandInput input)
        {
            var command = Command.Create(input.CmdName, input.Cmd, input.CmdState, input.NodeId, input.TaskId);
            _commandRepository.Insert(command);
            return command.Id;
        }

        public List<CommandListOutput> GetList(CommandListInput input)
        {
            var commands =
                _commandRepository.GetAllList().OrderBy(a => a.Id).Skip(input.iDisplayStart).Take(input.iDisplayLength);
            return commands.MapTo<List<CommandListOutput>>();
        }

        public int GetListTotal(CommandListInput input)
        {
            return _commandRepository.GetAllList().Count;
        }
    }
}
