using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace TaskManager
{
    public abstract class BaseEntity : FullAuditedEntity
    {
        [Column("id")]
        [Description("自增主键")]
        public override int Id { get; set; }

        [Column("is_deleted")]
        [Description("是否删除")]
        public override bool IsDeleted { get; set; }

        [Column("deleter_user_id")]
        [Description("删除人ID")]
        public override long? DeleterUserId { get; set; }

        [Column("deletion_time")]
        [Description("删除时间")]
        public override DateTime? DeletionTime { get; set; }

        [Column("last_modification_time")]
        [Description("修改时间")]
        public override DateTime? LastModificationTime { get; set; }

        [Column("last_modifier_user_id")]
        [Description("修改人ID")]
        public override long? LastModifierUserId { get; set; }

        [Column("creation_time")]
        [Description("创建时间")]
        public override DateTime CreationTime { get; set; }

        [Column("creator_user_id")]
        [Description("创建人ID")]
        public override long? CreatorUserId { get; set; }
    }
}
