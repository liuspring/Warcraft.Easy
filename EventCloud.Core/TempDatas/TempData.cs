using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Tasks;

namespace TaskManager.TempDatas
{
    [Table("qrtz_temp_data")]
    [Description("任务数据库中的临时数据表")]
    public class TempData : BaseEntity
    {

        [ForeignKey("TaskId")]
        public Task Task { get; set; }

        [Column("task_id")]
        [Description("任务ID")]
        public int TaskId { get; set; }

        [Column("data_json")]
        [StringLength(50)]
        [Description("数据库临时数据json")]
        public string DataJson { get; set; }

    }
}
