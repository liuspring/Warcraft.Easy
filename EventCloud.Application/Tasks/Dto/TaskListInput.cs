using System.Web;

namespace EventCloud.Tasks.Dto
{
    public class TaskListInput : DataTablesRequest
    {
        public TaskListInput(HttpRequestBase request) : base(request)
        {
        }

        public TaskListInput(HttpRequest httpRequest) : base(httpRequest)
        {
        }
    }
}
