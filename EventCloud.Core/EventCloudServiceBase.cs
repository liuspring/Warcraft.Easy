using Abp;

namespace TaskManager
{
    public class TaskManagerServiceBase : AbpServiceBase
    {
        public TaskManagerServiceBase()
        {
            LocalizationSourceName = TaskManagerConsts.LocalizationSourceName;
        }
    }
}