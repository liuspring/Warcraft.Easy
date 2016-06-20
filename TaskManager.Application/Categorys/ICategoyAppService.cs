using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Categorys.Dto;

namespace TaskManager.Categorys
{
    public interface ICategoyAppService
    {
        int Create(CreateCategoryInput input);

        List<CategoryListOutput> GetList(CategoryListInput input);
        int GetListTotal(CategoryListInput input);

        List<Category> GetAllList();
    }
}
