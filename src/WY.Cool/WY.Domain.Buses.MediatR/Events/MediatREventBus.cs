using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Domain.Abstractions.Events;

namespace WY.Domain.Buses.MediatR.Events
{
    /// <summary>
    /// MediatR事件总线
    /// </summary>
    public class MediatREventBus : IEventBus
    {
        protected readonly IMediator mediator;
        protected readonly IEventStore eventStore;

        public MediatREventBus(IMediator mediator, IEventStore eventStore)
        {
            this.mediator = mediator;
            this.eventStore = eventStore;
        }

        public void PublishEvent(IEvent @event)
        {
            PublishEventAsync(@event).Wait();
        }

        public Task PublishEventAsync(IEvent @event, CancellationToken cancellationToken = default)
        {
            return Task.WhenAll(eventStore?.SaveAsync(@event, cancellationToken) ?? Task.CompletedTask, mediator.Publish(@event, cancellationToken));
        }
    }
}
