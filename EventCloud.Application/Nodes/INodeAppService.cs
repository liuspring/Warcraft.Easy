using System.Collections.Generic;
using EventCloud.Nodes.Dto;

namespace EventCloud.Nodes
{
    public interface INodeAppService
    {
        int Create(CreateNodeInput input);

        List<NodeListOutput> GetList(NodeListInput input);

        int GetListTotal(NodeListInput input);

        List<Node> GetAllList();

    }
}
