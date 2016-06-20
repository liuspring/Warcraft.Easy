using System.Web;

namespace TaskManager.Users.Dto
{
    public class UserListInput:DataTablesRequest
    {
        public UserListInput(HttpRequestBase request) : base(request)
        {
        }

        public UserListInput(HttpRequest httpRequest) : base(httpRequest)
        {
        }
    }
}
