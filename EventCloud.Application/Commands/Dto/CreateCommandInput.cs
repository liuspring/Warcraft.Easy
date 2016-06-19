using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace EventCloud.Commands.Dto
{
    [AutoMapFrom(typeof(Command))]
    public class CreateCommandInput
    {
        [Required]
        public string CmdName { get; set; }

        [Required]
        public string Cmd { get; set; }


        [Required]
        public byte CmdState { get; set; }

        [Required]

        public int NodeId { get; set; }

        [Required]

        public int TaskId { get; set; }

        public CreateCommandInput()
        {
            CmdState = 0;
        }

    }
}
