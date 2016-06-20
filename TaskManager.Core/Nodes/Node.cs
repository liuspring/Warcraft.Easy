using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TaskManager.Nodes
{
    [Table("qrtz_node")]
    public class Node : BaseEntity
    {
        [Column("id")]
        [Description("自增主键")]
        public override int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("node_name")]
        [Description("节点名")]
        public string NodeName { get; set; }

        [Required]
        [StringLength(20)]
        [Column("node_ip")]
        [Description("节点ip/host")]
        public string NodeIp { get; set; }

        [Required]
        [Column("if_check_state")]
        [Description("是否检查状态")]
        public bool IfCheckState { get; set; }

        /// <summary>
        /// 任务命令集合
        /// </summary>
        [ForeignKey("NodeId")]
        public virtual ICollection<Commands.Command> Commands { get; protected set; }

        /// <summary>
        /// 错误日志集合
        /// </summary>
        [ForeignKey("NodeId")]
        public virtual ICollection<Errors.Error> Errors { get; protected set; }

        /// <summary>
        /// 一般日志集合
        /// </summary>
        [ForeignKey("NodeId")]
        public virtual ICollection<Logs.Log> Logs { get; protected set; }

        /// <summary>
        /// 一般日志集合
        /// </summary>
        [ForeignKey("NodeId")]
        public virtual ICollection<Performances.Performance> Performances { get; protected set; }

        protected Node()
        {
        }

        public static Node Create(string nodeName, string nodeIp)
        {
            var node = new Node
            {
                NodeName = nodeName,
                NodeIp = nodeIp
            };
            return node;
        }
    }
}
