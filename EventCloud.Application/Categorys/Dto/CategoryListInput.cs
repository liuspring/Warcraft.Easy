using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EventCloud.Categorys.Dto
{
    public class CategoryListInput : DataTablesRequest
    {
        public CategoryListInput(HttpRequestBase request) : base(request)
        {
        }

        public CategoryListInput(HttpRequest httpRequest) : base(httpRequest)
        {
        }
    }
}
