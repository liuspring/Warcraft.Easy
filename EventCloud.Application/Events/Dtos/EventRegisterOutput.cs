using Abp.Application.Services.Dto;

namespace TaskManager.Events.Dtos
{
    public class EventRegisterOutput : IOutputDto
    {
        public int RegistrationId { get; set; }
    }
}