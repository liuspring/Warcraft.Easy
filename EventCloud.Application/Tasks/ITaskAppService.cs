using System.Collections.Generic;
using EventCloud.Tasks.Dto;

namespace EventCloud.Tasks
{
    public interface ITaskAppService
    {
        int Create(CreateTaskInput input);

        List<TaskListOutput> GetList(TaskListInput input);

        int GetListTotal(TaskListInput input);

        List<Task> GetAllList();
    }
}
