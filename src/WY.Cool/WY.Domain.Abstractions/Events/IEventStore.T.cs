using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.Domain.Abstractions.Events;

/// <summary>
/// 有返回值的事件存储
/// </summary>
/// <typeparam name="TResult">返回值的类型</typeparam>
public interface IEventStore<TResult> : IEventStore
{
    /// <summary>
    /// 保存事件
    /// </summary>
    /// <param name="event">事件实例</param>
    /// <returns>返回值</returns>
    new TResult? Save(IEvent @event);

    /// <summary>
    /// 保存事件
    /// </summary>
    /// <param name="event">事件实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>获取返回值的任务</returns>
    new Task<TResult?> SaveAsync(IEvent @event, CancellationToken cancellationToken = default);
}
