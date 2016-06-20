using Abp.Application.Services.Dto;

namespace TaskManager.Events.Dtos
{
    public class GetEventListInput : IInputDto
    {
        public bool IncludeCanceledEvents { get; set; }
    }
}