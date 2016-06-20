using TaskManager.Nodes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Tasks;

namespace TaskManager.Logs
{
    [Table("qrtz_log")]
    [Description("一般日志表")]
    public class Log : BaseEntity
    {
        [ForeignKey("NodeId")]
        public Node Node { get; set; }

        [Required]
        [Column("node_id")]
        [Description("节点ID")]
        public int NodeId { get; set; }

        [ForeignKey("TaskId")]
        public Task Task { get; set; }

        [Column("task_id")]
        [Description("任务ID")]
        public int TaskId { get; set; }

        [Required]
        [StringLength(4000)]
        [Column("mgs")]
        [Description("错误信息")]
        public string Msg { get; set; }

        [Required]
        [Column("log_type")]
        [Description("日志类型")]
        public byte LogType { get; set; }
    }
}
