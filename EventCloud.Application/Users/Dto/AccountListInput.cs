using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EventCloud.Users.Dto
{
    public class AccountListInput:DataTablesRequest
    {
        public AccountListInput(HttpRequestBase request) : base(request)
        {
        }

        public AccountListInput(HttpRequest httpRequest) : base(httpRequest)
        {
        }
    }
}
