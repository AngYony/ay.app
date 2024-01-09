using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Domain.Abstractions.Events;

namespace WY.Domain.Buses.MediatR.Events;


/// <summary>
/// 有返回值的MediatR事件总线
/// </summary>
/// <typeparam name="TResult">返回值类型</typeparam>
public class MediatREventBus<TResult> : MediatREventBus, IEventBus<TResult>
{
    public MediatREventBus(IMediator mediator, IEventStore eventStore)
        : base(mediator, eventStore)
    {
    }

    TResult? IEventBus<TResult>.PublishEvent(IEvent @event)
    {
        PublishEventAsync(@event).GetAwaiter().GetResult();
        return default;
    }

    Task<TResult?> IEventBus<TResult>.PublishEventAsync(IEvent @event, CancellationToken cancellationToken)
    {
        eventStore?.SaveAsync(@event, cancellationToken);
        mediator.Publish(@event, cancellationToken);
        return Task.FromResult(default(TResult));
    }
}