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
