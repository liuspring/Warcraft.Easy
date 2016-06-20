using System.Web;

namespace TaskManager.Tasks.Dto
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
