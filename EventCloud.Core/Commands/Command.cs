using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventCloud.Nodes;
using EventCloud.Tasks;

namespace EventCloud.Commands
{
    [Table("qrtz_command")]
    [Description("任务命令表")]

    public class Command : BaseEntity
    {

        [Required]
        [StringLength(400)]
        [Column("cmd")]
        [Description("命令JSON")]
        public string Cmd { get; set; }

        [Required]
        [StringLength(20)]
        [Column("cmd_name")]
        [Description("命令名称")]
        public string CmdName { get; set; }

        [Required]
        [Column("cmd_state")]
        [Description("命令状态")]
        public byte CmdState { get; set; }

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

    }
}
