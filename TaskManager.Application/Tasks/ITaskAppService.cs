using System.Collections.Generic;
using TaskManager.Tasks.Dto;

namespace TaskManager.Tasks
{
    public interface ITaskAppService
    {
        int Create(CreateTaskInput input);

        List<TaskListOutput> GetList(TaskListInput input);

        int GetListTotal(TaskListInput input);

        List<Task> GetAllList();
    }
}
