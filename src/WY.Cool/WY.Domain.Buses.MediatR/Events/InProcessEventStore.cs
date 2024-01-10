using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Domain.Abstractions.Events;

namespace WY.Domain.Buses.MediatR.Events;


/// <summary>
/// 进程内事件存储，只写入日志，该类用于扩展使用，实际应用中根据需要可移除
/// </summary>
public class InProcessEventStore : IEventStore
{
    protected readonly ILogger _logger;

    public InProcessEventStore(ILogger<InProcessEventStore> logger)
    {
        _logger = logger;
    }

    public void Save(IEvent @event)
    {
        SaveAsync(@event).Wait();
    }

    public Task SaveAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("{datetime} 发布了事件 {commandId}", @event.Timestamp, @event.Id);
        return Task.CompletedTask;
    }
}
