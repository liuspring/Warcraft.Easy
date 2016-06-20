using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using EventCloud.Tasks;

namespace EventCloud.Versions
{
    [Table("qrtz_version_info")]
    [Description("任务版本库")]
    public class VersionInfo : BaseEntity
    {

        [ForeignKey("TaskId")]
        public Task Task { get; set; }

        [Column("task_id")]
        [Description("任务ID")]
        public int TaskId { get; set; }

        [Column("version_type")]
        [Description("版本")]
        public int VersionType { get; set; }

        [Column("zip_file")]
        [Description("上传文件")]
        public byte[] ZipFile { get; set; }

        [Column("zip_filename")]
        [StringLength(100)]
        [Description("上传文件名称")]
        public string ZipFileName { get; set; }

        [Column("zip_filepath")]
        [StringLength(200)]
        [Description("上传文件路径")]
        public string ZipFilePath { get; set; }

        public static VersionInfo Create(int taskId, string zipFileName, string zipFilePath)
        {
            var versionInfo = new VersionInfo
            {
                TaskId = taskId,
                VersionType = 0,
                ZipFileName = zipFileName,
                ZipFilePath = zipFilePath
            };

            FileStream fs = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + zipFilePath);
            var buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            versionInfo.ZipFile = buffer;
            return versionInfo;
        }

    }
}
