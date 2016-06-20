using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using TaskManager.Categorys;

namespace TaskManager.Tasks.Dto
{
    [AutoMapFrom(typeof(Task))]
    public class TaskDetailOutput : BaseEntity, IOutputDto
    {
        public string TaskName { get; set; }

        public int CategoryId { get; set; }

        public int NodeId { get; set; }

        public byte State { get; set; }

        public int Version { get; set; }

        public string AppConfigJson { get; set; }

        public string Cron { get; set; }

        public string MainClassDllFileName { get; set; }

        public string MainClassNameSpace { get; set; }

        public string Remark { get; set; }
    }
}
