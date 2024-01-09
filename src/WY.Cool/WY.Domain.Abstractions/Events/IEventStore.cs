using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.Domain.Abstractions.Events;

/// <summary>
/// 事件存储
/// </summary>
public interface IEventStore
{
    /// <summary>
    /// 保存事件
    /// </summary>
    /// <param name="event">事件实例</param>
    void Save(IEvent @event);

    /// <summary>
    /// 保存事件
    /// </summary>
    /// <param name="event">事件实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>指示事件保存状态的任务</returns>
    Task SaveAsync(IEvent @event, CancellationToken cancellationToken = default);
}
