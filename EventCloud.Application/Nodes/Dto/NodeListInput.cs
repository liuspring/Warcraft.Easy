using System.Web;

namespace TaskManager.Nodes.Dto
{
    public class NodeListInput: DataTablesRequest
    {
        public NodeListInput(HttpRequestBase request): base(request)
        {
        }

        public NodeListInput(HttpRequest httpRequest): base(httpRequest)
        {
        }
    }
}
