using Abp.Events.Bus.Entities;

namespace TaskManager.Events
{
    public class EventCancelledEvent : EntityEventData<Event>
    {
        public EventCancelledEvent(Event entity) 
            : base(entity)
        {
        }
    }
}