using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using EventCloud.Tasks.Dto;

namespace EventCloud.Tasks
{
    public class TaskAppService : EventCloudAppServiceBase, ITaskAppService
    {

        private readonly IRepository<Task, int> _taskRepository;

        public TaskAppService(IRepository<Task, int> taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public int Create(CreateTaskInput input)
        {
            var task = Task.Create(input.TaskName, input.CategoryId, input.NodeId, input.State, input.Version,
                input.AppConfigJson,
                input.Cron, input.MainClassDllFileName, input.MainClassNameSpace, input.Remark);
            _taskRepository.Insert(task);
            return task.Id;
        }

        public List<TaskListOutput> GetList(TaskListInput input)
        {
            var tasks =
                _taskRepository.GetAllList().OrderBy(a => a.Id).Skip(input.iDisplayStart).Take(input.iDisplayLength);
            return tasks.MapTo<List<TaskListOutput>>();
        }

        public int GetListTotal(TaskListInput input)
        {
            return _taskRepository.GetAllList().Count;
        }
    }
}
