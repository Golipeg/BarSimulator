using SimpleEventBus;
using SimpleEventBus.Interfaces;

namespace Events
{
    public static class EventStreams
    {
        public static IEventBus GameEventBus { get; } = new EventBus();
    }
}