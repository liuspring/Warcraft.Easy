using System.Web;

namespace TaskManager.Categorys.Dto
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
