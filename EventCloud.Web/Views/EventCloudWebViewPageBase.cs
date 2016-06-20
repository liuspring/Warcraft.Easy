using Abp.Web.Mvc.Views;

namespace TaskManager.Web.Views
{
    public abstract class TaskManagerWebViewPageBase : TaskManagerWebViewPageBase<dynamic>
    {

    }

    public abstract class TaskManagerWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected TaskManagerWebViewPageBase()
        {
            LocalizationSourceName = TaskManagerConsts.LocalizationSourceName;
        }
    }
}