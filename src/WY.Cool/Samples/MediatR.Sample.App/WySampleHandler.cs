using WY.Domain.Buses.MediatR.Events;

namespace MediatR.Sample.App
{
    /// <summary>
    /// 接受消息处理程序
    /// </summary>
    public class WySampleHandler : MediatREventHandler<WySampleEvent>
    {
        readonly ILogger _logger;
        public WySampleHandler(ILogger<WySampleHandler> logger)
        {
            _logger = logger;
        }
        public override Task Handle(WySampleEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogWarning("接受消息进行处理1");
            return Task.CompletedTask;
        }
    }


    public class WySampleHandler2 : MediatREventHandler<WySampleEvent>
    {
        readonly ILogger _logger;
        public WySampleHandler2(ILogger<WySampleHandler2> logger)
        {
            _logger = logger;
        }
        public override Task Handle(WySampleEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogWarning("接受消息进行处理2");
            return Task.CompletedTask;
        }
    }
}
