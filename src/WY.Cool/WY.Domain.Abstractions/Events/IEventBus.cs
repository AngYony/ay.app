namespace WY.Domain.Abstractions.Events;

/// <summary>
/// 事件总线
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="event">事件实例</param>
    void PublishEvent(IEvent @event);

    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="event">事件实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>指示事件发布状态的任务</returns>
    Task PublishEventAsync(IEvent @event, CancellationToken cancellationToken=default);
}