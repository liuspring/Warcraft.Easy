using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace TaskManager.Tasks.Dto
{
    [AutoMapFrom(typeof(Task))]
    public class CreateTaskInput:IInputDto
    {
        [Required]
        [StringLength(50)]
        public string TaskName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int NodeId { get; set; }

        public byte State { get; set; }

        public int Version { get; set; }

        [Required]
        public string AppConfigJson { get; set; }

        [Required]
        public string Cron { get; set; }

        [Required]
        [StringLength(100)]
        public string MainClassDllFileName { get; set; }

        [Required]
        [StringLength(100)]
        public string MainClassNameSpace { get; set; }

        [Required]
        [StringLength(2000)]
        public string Remark { get; set; }

        [Required]
        public string FileZipPath { get; set; }

        [Required]
        public string FileZipName { get; set; }

    }
}
