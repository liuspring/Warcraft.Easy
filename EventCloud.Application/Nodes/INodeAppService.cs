using System.Collections.Generic;
using TaskManager.Nodes.Dto;

namespace TaskManager.Nodes
{
    public interface INodeAppService
    {
        int Create(CreateNodeInput input);

        List<NodeListOutput> GetList(NodeListInput input);

        int GetListTotal(NodeListInput input);

        List<Node> GetAllList();

    }
}
