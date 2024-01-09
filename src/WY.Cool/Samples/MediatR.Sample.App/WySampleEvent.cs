using WY.Domain.Buses.MediatR.Events;

namespace MediatR.Sample.App
{
    public class WySampleEvent : MediatREvent
    {
        public string Name { get; }
        public int Age { get; }

        public WySampleEvent(string name, int age, string? eventId = null) : base(eventId)
        {
            Name = name;
            Age = age;
        }
    }
}
