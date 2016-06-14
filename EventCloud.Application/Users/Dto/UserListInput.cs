using System.Web;

namespace EventCloud.Users.Dto
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
