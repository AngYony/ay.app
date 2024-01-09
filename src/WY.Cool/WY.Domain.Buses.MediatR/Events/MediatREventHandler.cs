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
    /// MediatR事件处理器基类
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    public abstract class MediatREventHandler<TEvent> : IEventHandler<TEvent>, INotificationHandler<TEvent>
        where TEvent : MediatREvent
    {
        public abstract Task Handle(TEvent @event, CancellationToken cancellationToken);
    }
}
