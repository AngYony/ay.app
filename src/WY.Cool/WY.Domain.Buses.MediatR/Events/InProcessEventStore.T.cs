using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Domain.Abstractions.Events;

namespace WY.Domain.Buses.MediatR.Events;


/// <summary>
/// 进程内事件存储，只写入日志
/// </summary>
/// <typeparam name="TResult">返回值类型</typeparam>
public class InProcessEventStore<TResult> : InProcessEventStore, IEventStore<TResult>
{
    public InProcessEventStore(ILogger<InProcessEventStore<TResult>> logger) : base(logger)
    {
    }

    TResult? IEventStore<TResult>.Save(IEvent @event)
    {
        Save(@event);
        return default;
    }

    async Task<TResult?> IEventStore<TResult>.SaveAsync(IEvent @event, CancellationToken cancellationToken)
    {
        await SaveAsync(@event, cancellationToken);
        return default(TResult);
    }
}

