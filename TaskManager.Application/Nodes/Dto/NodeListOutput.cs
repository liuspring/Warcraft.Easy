using System;
using Abp.AutoMapper;

namespace TaskManager.Nodes.Dto
{
    [AutoMapFrom(typeof(Node))]
    public class NodeListOutput
    {
        public int Id { get; set; }

        public string NodeName { get; set; }

        public string NodeIp { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public bool IfRunning
        {
            get
            {
                if (LastModificationTime == null) return false;
                return (DateTime.Now - LastModificationTime.Value) < TimeSpan.FromSeconds(10);
            }
        }

        public bool IfCheckState { get; set; }
    }
}
