using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using EventCloud.Tasks.Dto;
using EventCloud.Versions;

namespace EventCloud.Tasks
{
    public class TaskAppService : EventCloudAppServiceBase, ITaskAppService
    {

        private readonly IRepository<Task, int> _taskRepository;
        private readonly IRepository<Versions.VersionInfo, int> _versionInfoRepository;


        public TaskAppService(IRepository<Task, int> taskRepository, IRepository<Versions.VersionInfo, int> versionInfoRepository)
        {
            _taskRepository = taskRepository;
            _versionInfoRepository = versionInfoRepository;
        }


        public int Create(CreateTaskInput input)
        {
            var task = Task.Create(input.TaskName, input.CategoryId, input.NodeId, input.State, input.Version,
                input.AppConfigJson,
                input.Cron, input.MainClassDllFileName, input.MainClassNameSpace, input.Remark);
            //var a= _taskRepository.Insert(task);
            var versionInfo = VersionInfo.Create(task.Id, input.FileZipName, input.FileZipPath);
            versionInfo.TaskId = 2;
            _versionInfoRepository.Insert(versionInfo);
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

        public List<Task> GetAllList()
        {
            return _taskRepository.GetAllList();
        }
    }
}
