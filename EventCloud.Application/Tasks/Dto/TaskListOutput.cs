using Abp.AutoMapper;

namespace EventCloud.Tasks.Dto
{
    [AutoMapFrom(typeof(Task))]
    public class TaskListOutput
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public int CategoryId { get; set; }

        public int CmdState { get; set; }

        public int NodeId { get; set; }

        public byte State { get; set; }

        public string Remark { get; set; }
    }
}
