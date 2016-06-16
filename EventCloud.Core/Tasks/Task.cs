using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventCloud.Tasks
{
    [Table("qrtz_task")]
    [Description("任务表")]

    public class Task : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("task_name")]
        [Description("任务名称")]
        public string TaskName { get; set; }

        [ForeignKey("CategoryId")]
        public Categorys.Category Category { get; set; }

        [Required]
        [Column("category_id")]
        [Description("列表ID")]
        public int CategoryId { get; set; }

        [Required]
        [Column("cmd_state")]
        [Description("命令状态")]
        public int CmdState { get; set; }

        [ForeignKey("NodeId")]
        public Nodes.Node Node { get; set; }

        [Required]
        [Column("node_id")]
        [Description("节点ID")]
        public int NodeId { get; set; }

        [Column("last_start_time")]
        [Description("最近开始时间")]
        public DateTime LastStartTime { get; set; }

        [Column("last_end_time")]
        [Description("最近结束时间")]
        public DateTime LastEndTime { get; set; }

        [Column("last_error_time")]
        [Description("上次出错时间")]
        public DateTime LastErrorTime { get; set; }

        [Column("error_count")]
        [Description("连续错误次数")]
        public int ErrorCount { get; set; }

        [Column("run_count")]
        [Description("运行成功次数")]
        public int RunCount { get; set; }

        [Column("state")]
        [Description("状态")]
        public byte State { get; set; }

        [Column("version")]
        [Description("版本")]
        public int Version { get; set; }

        [Required]
        [StringLength(1000)]
        [Column("app_config_json")]
        [Description("配置json")]
        public string AppConfigJson { get; set; }

        [Required]
        [StringLength(50)]
        [Column("cron")]
        [Description("Cron表达式")]
        public string Cron { get; set; }

        [Required]
        [StringLength(100)]
        [Column("mainclass_dll_filename")]
        [Description("任务入口类的命名空间")]
        public string MainClassDllFileName{ get; set; }

        [Required]
        [StringLength(100)]
        [Column("mainclass_namespace")]
        [Description("任务入口dll文件名")]
        public string MainClassNameSpace { get; set; }


        [Required]
        [StringLength(2000)]
        [Column("remark")]
        [Description("备注")]
        public string Remark { get; set; }


        /// <summary>
        /// 任务命令集合
        /// </summary>
        [ForeignKey("TaskId")]
        public virtual ICollection<Commands.Command>  Commands{ get; protected set; }

        /// <summary>
        /// 错误日志集合
        /// </summary>
        [ForeignKey("TaskId")]
        public virtual ICollection<Errors.Error> Errors { get; protected set; }

        /// <summary>
        /// 一般日志集合
        /// </summary>
        [ForeignKey("TaskId")]
        public virtual ICollection<Logs.Log> Logs { get; protected set; }

        /// <summary>
        /// 节点和任务性能记录表集合
        /// </summary>
        [ForeignKey("TaskId")]
        public virtual ICollection<Performances.Performance> Performances { get; protected set; }

        /// <summary>
        /// 节点和任务性能记录表集合
        /// </summary>
        [ForeignKey("TaskId")]
        public virtual ICollection<TempDatas.TempData> TempDatas { get; protected set; }

        /// <summary>
        /// 任务版本库集合
        /// </summary>
        [ForeignKey("TaskId")]
        public virtual ICollection<Versions.VersionInfo> Versions { get; protected set; }
    }
}
