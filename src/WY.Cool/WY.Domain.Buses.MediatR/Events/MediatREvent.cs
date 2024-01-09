using MediatR;
using WY.Domain.Abstractions.Events;
using WY.Utilities.TypeExtensions;

namespace WY.Domain.Buses.MediatR.Events
{

    /// <summary>
    /// MediatR事件基类
    /// </summary>
    public abstract class MediatREvent : IEvent, INotification
    {
        public string Id { get; }

        public DateTimeOffset Timestamp { get; }

        public MediatREvent(string? eventId = null)
        {
            if (eventId is not null && eventId.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("MediatREvent：eventId值无效", nameof(eventId));
            }
            Id = eventId ?? Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.Now;
        }
    }
}